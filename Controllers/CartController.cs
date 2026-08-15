using E_commerce_iti.Models;
using E_commerce_iti.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_iti.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartServices _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(
            CartServices cartService,
            UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyCart()
        {
            var userId = _userManager.GetUserId(User);

            var cart = await _cartService.GetCartAsync(int.Parse(userId!));

            if (cart == null)
                return NotFound("Cart not found.");

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int productId)
        {
            var userId = _userManager.GetUserId(User);

            var result = await _cartService.AddItemAsync(
                int.Parse(userId!),
                productId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(MyCart));
        }

        [HttpPost]
        public async Task<IActionResult> Increase(int itemId)
        {
            var userId = _userManager.GetUserId(User);

            var result = await _cartService.IncreaseAsync(
                int.Parse(userId!),
                itemId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(MyCart));
        }

        [HttpPost]
        public async Task<IActionResult> Decrease(int itemId)
        {
            var userId = _userManager.GetUserId(User);

            var result = await _cartService.DecreaseAsync(
                int.Parse(userId!),
                itemId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(MyCart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var userId = _userManager.GetUserId(User);

            var result = await _cartService.RemoveItemAsync(
                int.Parse(userId!),
                itemId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(MyCart));
        }
    }
}