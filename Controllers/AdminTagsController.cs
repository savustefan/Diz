using LucrareDisertatie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LucrareDisertatie.Data;

using LucrareDisertatie.Models.Domain;
using Microsoft.EntityFrameworkCore;
using LucrareDisertatie.Repositories;

namespace LucrareDisertatie.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SaveTag(SaveTagRequest saveTagRequest)
        {
            //mapping SaveTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = saveTagRequest.Name,
                DisplayedName = saveTagRequest.DisplayedName
            };

            await _tagRepository.AddTagAsync(tag);

            return RedirectToAction("ListTag");
        }

        [HttpGet]
        [ActionName("ListTag")]
        public async Task<IActionResult> ListTag()
        {
            var allTags = await _tagRepository.GetAllTagsAsync();

            return View(allTags);
        }

        [HttpGet]
        public async Task <IActionResult> EditTag(Guid id)
        {
            var tag = await _tagRepository.GetTagAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> EditTag(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayedName = editTagRequest.DisplayedName
            };

            var updatedTag = await _tagRepository.UpdateTagAsync(tag);
            
            if (updatedTag != null)
            {
                //succes
            }
            else
            {
                //error
            }

            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteTagAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //succes
                return RedirectToAction("ListTag");
            }

            //error notif
            return RedirectToAction("EditTag", new { id = editTagRequest.Id });
        }

    }
}
