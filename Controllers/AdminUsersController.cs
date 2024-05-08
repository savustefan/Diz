using LucrareDisertatie.Models.ViewModels;
using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LucrareDisertatie.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserManager<IdentityUser> _UserManager { get; }

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager )
        {
            _userRepository = userRepository;
            _UserManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var allUsers = await _userRepository.GetAll();

            var usersList = new UserViewModel();
            usersList.Users = new List<User>();


            foreach (var user in allUsers)
            {
                usersList.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }

            return View(usersList);
        }

        [HttpPost]

        public async Task<IActionResult> List(UserViewModel userViewRequest)
        {
            var identityUserManager = new IdentityUser
            {
                UserName = userViewRequest.Username,
                Email = userViewRequest.EmailAdress
            };

            var identityResult = await _UserManager.CreateAsync(identityUserManager, userViewRequest.Password);

            if(identityResult != null)
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if (userViewRequest.CreatorRoleCheckbox)
                    {
                        roles.Add("Creator");
                    }

                    identityResult = await _UserManager.AddToRolesAsync(identityUserManager, roles);

                    if (identityResult != null && identityResult.Succeeded)
                    {
                        return RedirectToAction("ListUsers", "AdminUsers");
                    }
                }
            }

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Guid id)
        {

            var userId = await _UserManager.FindByIdAsync(id.ToString());

            if (userId != null)
            {
                var identityResult = await _UserManager.DeleteAsync(userId);

                if (identityResult != null && identityResult.Succeeded)
                {
                    return RedirectToAction("ListUsers", "AdminUsers");
                }
            }

            return View();
        }
    }
}
