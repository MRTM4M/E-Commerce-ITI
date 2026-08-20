using E_commerce_iti.Data;
using E_commerce_iti.Services;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserServices _userServices;
        private readonly CartServices _cartServices;
        private readonly IProductService _productService;
        private readonly    ECommerceDB _context;

        public AdminController(
            UserServices userServices,
            CartServices cartServices,
            IProductService productService,
            ECommerceDB context)
        {
            _userServices = userServices;
            _cartServices = cartServices;
            _productService = productService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userServices.GetAllUsers();

            return View(users);
        }
        public async Task<IActionResult> UsersActive()
        {
            var users = await _userServices.GetAllActiveUsers();

            return View(users);
        }
        public async Task<IActionResult> UsersInactive()
        {
            var users = await _userServices.GetAllInactiveUsers();

            return View(users);
        }

        public async Task<IActionResult> Carts()
        {
            var carts = await _cartServices.GetAllCartsAsync();

            return View(carts);
        }

        public async Task<IActionResult> CartDetails(int id)
        {
            var cart = await _cartServices.GetCartByIdAsync(id);

            if (cart == null)
                return NotFound();

            return View(cart);
        }
        // =========================
        // Products
        // =========================

        // GET: /Admin/Products
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetAllAsync();

            return View(products);
        }

        // GET: /Admin/ProductDetails/5
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: /Admin/SearchProduct?id=5
        public async Task<IActionResult> SearchProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View("ProductDetails", product);
        }

        // GET: /Admin/CreateProduct
        public IActionResult CreateProduct()
        {
            return View();
        }

        // POST: /Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _productService.CreateAsync(model);

                return RedirectToAction(nameof(Products));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(model);
            }
        }

        // GET: /Admin/EditProduct/5
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(
            int id,
            ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _productService.UpdateAsync(model);

                return RedirectToAction(nameof(Products));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(model);
            }
        }

        // GET: /Admin/DeleteProduct/5
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: /Admin/DeleteProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            await _productService.DeleteAsync(id);

            return RedirectToAction(nameof(Products));
        }
    }
}

