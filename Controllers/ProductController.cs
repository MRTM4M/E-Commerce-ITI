using E_Commerce_iti.Models;
using Microsoft.AspNetCore.Mvc;
namespace E_Commerce_iti.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>();
        
        public IActionResult Index()
        {
            return View(products);
        }
    
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var oldProduct = products.FirstOrDefault(p => p.Id == product.Id);
                if (oldProduct == null)
                {
                    return NotFound();
                }
                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.Image = product.Image;
                oldProduct.Description = product.Description;
                oldProduct.Category = product.Category;
                oldProduct.Quantity = product.Quantity;
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return RedirectToAction("Index");
        }
    }
}