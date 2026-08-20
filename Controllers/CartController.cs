using E_commerce_iti.Services;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_commerce_iti.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            var cartItems = await _cartService.GetUserCartItemsAsync(userId);
            return View(cartItems);
        }

       
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminCartList()
        {
            var allCartItems = await _cartService.GetAllCartItemsForAdminAsync();
            return View(allCartItems);
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(CartItemViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            await _cartService.AddToCartAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(int id, int quantity)
        {
            await _cartService.UpdateQuantityAsync(id, quantity);
            return RedirectToAction(nameof(Index));
        }

       
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _cartService.RemoveFromCartAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}