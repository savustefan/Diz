using LucrareDisertatie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LucrareDisertatie.Data;

using LucrareDisertatie.Models.Domain;

namespace LucrareDisertatie.Controllers
{
    public class AdminTagsController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        public AdminTagsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;  
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SaveTag(SaveTagRequest saveTagRequest)
        {
            //mapping SaveTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = saveTagRequest.Name,
                DisplayedName = saveTagRequest.DisplayedName
            };

            _applicationDbContext.Tags.Add(tag);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("ListTag");
        }

        [HttpGet]
        public IActionResult ListTag()
        {
            var allTags = _applicationDbContext.Tags.ToList();

            return View(allTags);
        }

        [HttpGet]
        public IActionResult EditTag(Guid id)
        {
            var tag = _applicationDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayedName = tag.DisplayedName
                };

                return View(editTagRequest);
            }

            return View(null);
        }
    }
}
