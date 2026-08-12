using E_Commerce_iti.Models;
using Microsoft.AspNetCore.Mvc;
namespace E_Commerce_iti.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>();
        private static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Women" },
            new Category { Id = 2, Name = "Men" },
            new Category { Id = 3, Name = "Kids" },
            new Category { Id = 4, Name = "Sports" },
            new Category { Id = 5, Name = "Casual" },
            new Category { Id = 6, Name = "Heels" }
        };
        public IActionResult Index()
        {
            foreach (var product in products)
            {
                product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
            }
            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
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
            if (ModelState.IsValid)
            {
                product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
                product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
                products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
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
            if (ModelState.IsValid)
            {
                var existingProduct = products.FirstOrDefault(p => p.Id == id);
                if (existingProduct == null)
                    return NotFound();
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Image = product.Image;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.Category =categories.FirstOrDefault(c => c.Id == product.CategoryId);
                existingProduct.Quantity = product.Quantity;
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}