using E_commerce_iti.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_iti.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserServices _userServices;
        private readonly CartServices _cartServices;

        public AdminController(
            UserServices userServices,
            CartServices cartServices)
        {
            _userServices = userServices;
            _cartServices = cartServices;
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
    }
}