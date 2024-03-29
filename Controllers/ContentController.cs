using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LucrareDisertatie.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string handleUrl)
        {
            var content = await _contentRepository.GetHandleUrlAsync(handleUrl);

            return View(content);
        }
    }
}
