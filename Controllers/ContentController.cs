using LucrareDisertatie.Models.Domain;
using LucrareDisertatie.Models.ViewModels;
using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LucrareDisertatie.Controllers
{
    
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IContentCommentsRepository _contentCommentsRepository;

        public ContentController(IContentRepository contentRepository, IRatingRepository ratingRepository, SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, IContentCommentsRepository contentCommentsRepository)
        {
            _contentRepository = contentRepository;
            _ratingRepository = ratingRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _contentCommentsRepository = contentCommentsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string handleUrl)
        {
            var liked = false;
            var content = await _contentRepository.GetHandleUrlAsync(handleUrl);
            var ratingDetailsViewModel = new ContentDetailsViewModel();

            if (content != null)
            {
                var ratingTotal = await _ratingRepository.GetTotalRatings(content.ID);

                if (_signInManager.IsSignedIn(User))
                {
                    var allRatings = await _ratingRepository.GetRatingsForContent(content.ID);

                    var userId = _userManager.GetUserId(User);

                    if (userId != null)
                    {
                        var ratingFromUser = allRatings.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = ratingFromUser != null;

                    }
                }

                var contentCommentsDomainModel = await _contentCommentsRepository.GetCommentsAsync(content.ID);

                var contentCommentsForView = new List<ContentComment>();

                foreach (var contentComments in contentCommentsDomainModel)
                {
                    contentCommentsForView.Add(new ContentComment
                    {
                        Description = contentComments.Description,
                        TimeAdded = contentComments.TimeAdded,
                        Username = (await _userManager.FindByIdAsync(contentComments.UserId.ToString())).UserName
                    });
                }
                
                ratingDetailsViewModel = new ContentDetailsViewModel
                {
                    ID = content.ID,
                    Content = content.Content,
                    Title = content.Title,
                    Author = content.Author,
                    ImageUrl = content.ImageUrl,
                    Summary = content.Summary,
                    PostDate = content.PostDate,
                    Hidden = content.Hidden,
                    Tags = content.Tags,
                    HandleUrl = content.HandleUrl,
                    Header = content.Header,
                    TotalRatings = ratingTotal,
                    Liked = liked,
                    Comments = contentCommentsForView
                };
            }

            return View(ratingDetailsViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Index(ContentDetailsViewModel contentDetailsViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var domainModel = new ContentPostComment
                {
                    ContentPostId = contentDetailsViewModel.ID,
                    Description = contentDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    TimeAdded = DateTime.Now
                };

                await _contentCommentsRepository.AddAsync(domainModel);

                return RedirectToAction("Index", "Content", new { handleUrl = contentDetailsViewModel.HandleUrl});
            }

            return View();
            
        }
    }
}
