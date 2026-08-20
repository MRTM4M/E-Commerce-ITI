using E_commerce_iti.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_iti.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Product
        public async Task<IActionResult> Index(
        string? search,
        int? categoryId,
        int page = 1)
        {
            const int pageSize = 6;

            var shop = await _productService.GetShopAsync(
                search,
                categoryId,
                page,
                pageSize);

            return View(shop);
        }

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: /Product/Filter?category=Electronics
        public async Task<IActionResult> Filter(string category)
        {
            var products = await _productService.FilterAsync(category);

            return View("Index", products);
        }

        // GET: /Product/Page?page=2
        public async Task<IActionResult> Page(int page = 1)
        {
            const int pageSize = 5;

            var products = await _productService.GetPagedAsync(
                page,
                pageSize);

            return View("Index", products);
        }
    }
}