using LucrareDisertatie.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LucrareDisertatie.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.User,
                Email = registerViewModel.Email,
            };

            var identityManager = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityManager.Succeeded)
            {
                var roleUser = await _userManager.AddToRoleAsync(identityUser, "User");

                if (roleUser.Succeeded)
                {
                    return RedirectToAction("Register");
                }


            }

            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var signIn = await _signInManager.PasswordSignInAsync(loginViewModel.User, loginViewModel.Password, false, false);

            if (signIn != null && signIn.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public IActionResult RestrictAction()
        {
            return View();
        }
    }
}
