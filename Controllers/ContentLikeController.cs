using LucrareDisertatie.Models.Domain;
using LucrareDisertatie.Models.ViewModels;
using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LucrareDisertatie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentLikeController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public ContentLikeController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task <IActionResult> AddRating([FromBody] AddRatingRequest addRatingRequest)
        {
            var model = new Rating
            {
                ContentPostId = addRatingRequest.ContentId,
                UserId = addRatingRequest.UserId

            };

            await _ratingRepository.AddRatingContent(model);

            return Ok();

        }

        [HttpGet]
        [Route("{ContentPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] Guid ContentPostId)
        {
            var totalLikes = await _ratingRepository.GetTotalRatings(ContentPostId);

            return Ok(totalLikes);
        }
    }
}
