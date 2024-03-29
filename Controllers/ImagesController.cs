using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LucrareDisertatie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var urlImage = await _imageRepository.UploadAsync(file);

            if (urlImage == null)
            {
                return Problem("Error", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = urlImage });
        }
    }
}
