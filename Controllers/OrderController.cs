using E_commerce_iti.Enum;
using E_commerce_iti.Models;
using E_commerce_iti.Services;
using E_commerce_iti.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CartServices _cartService;

        public OrderController(
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            CartServices cartService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _cartService = cartService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated properly.");
            }

            return userId;
        }

        // ============================
        // User Actions
        // ============================

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();

            var orders = await _orderService.GetUserOrdersAsync(userId);

            var viewModel = orders.Select(o => new OrderIndexViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                ShippingCity = o.ShippingCity,
                ItemsCount = o.Items.Count
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var viewModel = new OrderDetailsViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                ShippingCity = order.ShippingCity,
                ShippingStreet = order.ShippingStreet,
                ShippingCountry = order.ShippingCountry,
                UserId = order.UserId,
                Items = order.Items.Select(i => new OrderItemDetailsViewModel
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "N/A",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new OrderFormViewModel
            {
                Items = new List<OrderItemFormViewModel> { new OrderItemFormViewModel() },
                AvailableProducts = await GetProductOptionsAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableProducts = await GetProductOptionsAsync();
                return View(model);
            }

            var order = new Order
            {
                UserId = GetCurrentUserId(),
                Status = model.Status,
                ShippingCity = model.ShippingCity,
                ShippingStreet = model.ShippingStreet,
                ShippingCountry = model.ShippingCountry,

                Items = model.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var success = await _orderService.CreateOrderAsync(order);

            if (!success)
            {
                ModelState.AddModelError(
                    "",
                    "error happened"
                );

                model.AvailableProducts = await GetProductOptionsAsync();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromCart()
        {
            var userIdString = _userManager.GetUserId(User);

            if (!int.TryParse(userIdString, out int userId))
                return Unauthorized();

            // Get current user's cart
            var cart = await _cartService.GetCartAsync(userId);

            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("MyCart", "Cart");
            }

            // Get current user with addresses
            var user = await _userManager.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return Unauthorized();

            // Get first address
            var address = user.Addresses.FirstOrDefault();

            if (address == null)
            {
                TempData["Error"] = "Please add an address before placing an order.";
                return RedirectToAction("MyCart", "Cart");
            }

            // Create Order
            var order = new Order
            {
                UserId = userId,

                Status = OrderStatus.Pending,

                ShippingCity = address.City,
                ShippingStreet = address.Street,
                ShippingCountry = address.Country,

                Items = cart.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            // Create order
            var success = await _orderService.CreateOrderAsync(order);

            if (!success)
            {
                TempData["Error"] = "Something went wrong while creating the order.";
                return RedirectToAction("MyCart", "Cart");
            }

            // Clear cart
            await _cartService.ClearCartAsync(userId);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            if (order.UserId != GetCurrentUserId() && !User.IsInRole("Admin"))
                return Forbid();

            var viewModel = new OrderFormViewModel
            {
                Id = order.Id,
                Status = order.Status,
                ShippingCity = order.ShippingCity,
                ShippingStreet = order.ShippingStreet,
                ShippingCountry = order.ShippingCountry,
                Items = order.Items.Select(i => new OrderItemFormViewModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList(),
                AvailableProducts = await GetProductOptionsAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderFormViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                model.AvailableProducts = await GetProductOptionsAsync();
                return View(model);
            }

            var existing = await _orderService.GetOrderByIdAsync(id);

            if (existing == null)
                return NotFound();

            if (existing.UserId != GetCurrentUserId() && !User.IsInRole("Admin"))
                return Forbid();

            if (existing.Status != OrderStatus.Pending)
                return BadRequest("This order can no longer be edited.");

            var order = new Order
            {
                Id = model.Id,
                UserId = existing.UserId,
                Status = model.Status,
                ShippingCity = model.ShippingCity,
                ShippingStreet = model.ShippingStreet,
                ShippingCountry = model.ShippingCountry,

                Items = model.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            var success = await _orderService.UpdateOrderAsync(order);

            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            if (order.UserId != GetCurrentUserId() && !User.IsInRole("Admin"))
                return Forbid();

            var viewModel = new OrderIndexViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                ShippingCity = order.ShippingCity,
                ItemsCount = order.Items.Count
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            if (order.UserId != GetCurrentUserId() && !User.IsInRole("Admin"))
                return Forbid();

            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<ProductOptionViewModel>> GetProductOptionsAsync()
        {
            var products = await _orderService.GetAllProductsAsync();
            return products.Select(p => new ProductOptionViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        // ============================
        // Admin Actions (Read-only + Change Status)
        // ============================

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            var viewModel = orders.Select(o => new OrderIndexViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                ShippingCity = o.ShippingCity,
                ItemsCount = o.Items.Count
            }).ToList();

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDetails(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var viewModel = new OrderDetailsViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                ShippingCity = order.ShippingCity,
                ShippingStreet = order.ShippingStreet,
                ShippingCountry = order.ShippingCountry,
                UserId = order.UserId,
                Items = order.Items.Select(i => new OrderItemDetailsViewModel
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "N/A",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var viewModel = new ChangeOrderStatusViewModel
            {
                Id = order.Id,
                CurrentStatus = order.Status,
                Status = order.Status
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, ChangeOrderStatusViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var success = await _orderService.UpdateOrderStatusAsync(id, model.Status);
            if (!success) return NotFound();

            return RedirectToAction(nameof(AdminIndex));
        }
    }
}