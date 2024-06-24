using Microsoft.AspNetCore.Mvc;
using ST10313014_P2.Models;
using ST10313014_P2.Models.DTOs;
using System.Diagnostics;
using System.Linq;

namespace ST10313014_P2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var allProducts = (await _homeRepository.GetProducts()).ToList();
            var randomProducts = allProducts.OrderBy(p => Guid.NewGuid()).Take(5).ToList();

            var model = new ProductDisplayModel
            {
                Products = randomProducts,
                Categories = await _homeRepository.Categories()
            };

            return View(model);
        }
        public async Task<IActionResult> MyWork(string sTerm = "", int categoryId = 0)
        {
            IEnumerable<Product> products = await _homeRepository.GetProducts(sTerm, categoryId);
            IEnumerable<Category> categories = await _homeRepository.Categories();
            ProductDisplayModel productModel = new ProductDisplayModel
            {
                Products = products,
                Categories = categories,
                STerm = sTerm,
                CategoryId = categoryId
            };
            return View(productModel);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
