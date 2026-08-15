using E_Commerce_iti.Models;
using E_Commerce_iti.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_iti.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryService.Create(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            _categoryService.Update(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}