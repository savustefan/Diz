using LucrareDisertatie.Models.Domain;
using LucrareDisertatie.Models.ViewModels;
using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LucrareDisertatie.Controllers
{
    public class AdminContentController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IContentRepository _contentRepository;

        public AdminContentController(ITagRepository tagRepository, IContentRepository contentRepository)
        {
            _tagRepository = tagRepository;
            _contentRepository = contentRepository;
        }


        [HttpGet]
       public async Task<IActionResult> Add()
        {
            //get tag from repo
            var tags = await _tagRepository.GetAllTagsAsync();

            var model = new AddContentRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayedName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddContentRequest addContentRequest)
        {

            //map view model to domain model

            var contentPostDomainModel = new ContentPost
            {
                Header = addContentRequest.Header,
                Title = addContentRequest.Title,
                Content = addContentRequest.Content,
                Summary = addContentRequest.Summary,
                ImageUrl = addContentRequest.ImageUrl,
                HandleUrl = addContentRequest.HandleUrl,
                PostDate = addContentRequest.PostDate,
                Author = addContentRequest.Author,
                Hidden = addContentRequest.Hidden,
            };

            //map taps from selected tags
            var selectedTags = new List<Tag>();

            foreach (var selectedTagId in addContentRequest.SelectedTags)
            {
                var selectedTagIdGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetTagAsync(selectedTagIdGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }


            //maping tags back to domain model
            contentPostDomainModel.Tags = selectedTags;


            await _contentRepository.AddAsync(contentPostDomainModel);


            return RedirectToAction("Add");
        }
    }
}
