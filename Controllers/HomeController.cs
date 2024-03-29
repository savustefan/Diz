using LucrareDisertatie.Models;
using LucrareDisertatie.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LucrareDisertatie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContentRepository _contentRepository;

        public HomeController(ILogger<HomeController> logger, IContentRepository contentRepository)
        {
            _logger = logger;
            _contentRepository = contentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Browse()
        {

            var allContent = await _contentRepository.GetAllAsync();

            return View(allContent);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
