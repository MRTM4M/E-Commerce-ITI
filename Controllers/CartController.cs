using E_commerce_iti.Data;
using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Controllers
{
    public class CartController : Controller
    {
        private readonly ECommerceDB context;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(
            ECommerceDB _context,
            UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }


        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id
                };

                context.Carts.Add(cart);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyCart));
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyCart()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var cart = await context.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
                return NotFound("Cart not found.");

            var model = new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                UserName = $"{user.FName} {user.LName}",

                Items = cart.CartItems.Select(ci => new CartItemViewModel
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                }).ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllCarts()
        {
            var carts = await context.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            var models = carts.Select(cart => new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                UserName = $"{cart.User.FName} {cart.User.LName}",

                Items = cart.CartItems.Select(ci => new CartItemViewModel
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                }).ToList()

            }).ToList();

            return View(models);
        }


        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> AddItem(int productId)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var cart = await context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id
                };

                context.Carts.Add(cart);
                await context.SaveChangesAsync();
            }

            var item = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (item == null)
            {
                item = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };

                context.CartItems.Add(item);
            }
            else
            {
                item.Quantity++;
            }

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(MyCart));
        }


        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Increase(int itemId)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var item = await context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci =>
                    ci.Id == itemId &&
                    ci.Cart.UserId == user.Id);

            if (item == null)
                return NotFound();

            item.Quantity++;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(MyCart));
        }


        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Decrease(int itemId)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var item = await context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci =>
                    ci.Id == itemId &&
                    ci.Cart.UserId == user.Id);

            if (item == null)
                return NotFound();

            if (item.Quantity > 1)
            {
                item.Quantity--;
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MyCart));
        }
       
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            var item = await context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci =>
                    ci.Id == itemId &&
                    ci.Cart.UserId == user.Id);

            if (item == null)
                return NotFound();

            context.CartItems.Remove(item);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(MyCart));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
                return NotFound();

            context.Carts.Remove(cart);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(AllCarts));
        }
    }
}