using E_Commerce_iti.Models;
using E_Commerce_iti.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_iti.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

      
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _productService.Create(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(product);

            _productService.Update(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string name)
        {
            var products = _productService.Search(name);

            return View("Index", products);
        }

        public IActionResult Filter(string category)
        {
            var products = _productService.Filter(category);

            return View("Index", products);
        }

        public IActionResult Page(int page = 1)
        {
            int pageSize = 5;

            var products = _productService.GetPaged(page, pageSize);

            return View("Index", products);
        }
    }
}