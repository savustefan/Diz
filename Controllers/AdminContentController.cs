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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // call repository
            var contentPosts = await _contentRepository.GetAllAsync();

            return View(contentPosts);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //retirve from repo
            var contentPost = await _contentRepository.GetAsync(id);
            var tagsDomainModel = await _tagRepository.GetAllTagsAsync();

            if (contentPost  != null)
            {
                var model = new EditContentRequest
                {
                    ID = contentPost.ID,
                    Header = contentPost.Header,
                    Title = contentPost.Title,
                    Content = contentPost.Content,
                    Summary = contentPost.Summary,
                    Author = contentPost.Author,
                    ImageUrl = contentPost.ImageUrl,
                    HandleUrl = contentPost.HandleUrl,
                    PostDate = contentPost.PostDate,
                    Hidden = contentPost.Hidden,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),

                    SelectedTags = contentPost.Tags.Select(x => x.Id.ToString()).ToArray()

                };

                return View(model);
            }

            //pass data to view
            return View(null);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditContentRequest editContentRequest)
        {
            //map view model back to domain model
            var contentDomainModel = new ContentPost
            {
                ID = editContentRequest.ID,
                Header = editContentRequest.Header,
                Title = editContentRequest.Title,
                Content = editContentRequest.Content,
                Summary = editContentRequest.Summary,
                Author = editContentRequest.Author,
                ImageUrl = editContentRequest.ImageUrl,
                HandleUrl = editContentRequest.HandleUrl,
                PostDate = editContentRequest.PostDate,
                Hidden = editContentRequest.Hidden,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editContentRequest.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _tagRepository.GetTagAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            contentDomainModel.Tags = selectedTags;

            //submit to repo to update
            var updatedContent = await _contentRepository.UpdateAsync(contentDomainModel);

            if (updatedContent != null)
            {
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");
            //redirect to get
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditContentRequest editContentRequest)
        {
            //communicate to repository

            var deletedContent= await _contentRepository.DeleteAsync(editContentRequest.ID);

            if (deletedContent != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editContentRequest.ID });
        }
    }
}
