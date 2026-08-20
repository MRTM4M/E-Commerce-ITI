using E_commerce_iti.Data;
using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class CartServices
    {
        private readonly ECommerceDB _context;

        public CartServices(ECommerceDB context)
        {
            _context = context;
        }

        private static CartViewModel MapToCartViewModel(Cart cart)
        {
            return new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                UserName = $"{cart.User.FName} {cart.User.LName}",

                Items = cart.CartItems
                    .Select(ci => new CartItemViewModel
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        ProductName = ci.Product.Name,
                        Price = ci.Product.Price,
                        Quantity = ci.Quantity
                    })
                    .ToList()
            };
        }
        public async Task<CartViewModel?> GetCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return null;

            return MapToCartViewModel(cart);
        }

        public async Task<CartViewModel?> GetCartByIdAsync(int cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
                return null;

            return MapToCartViewModel(cart);
        }

        public async Task<ICollection<CartViewModel>> GetAllCartsAsync()
        {
            var carts = await _context.Carts
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            return carts.Select(MapToCartViewModel).ToList();
        }

        public async Task<bool> CreateCartAsync(int userId)
        {
            var existingCart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (existingCart != null)
                return true;

            var cart = new Cart
            {
                UserId = userId
            };

            _context.Carts.Add(cart);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddItemAsync(
            int userId,
            int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return false;

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return false;

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

                _context.CartItems.Add(item);
            }
            else
            {
                item.Quantity++;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IncreaseAsync(
            int userId,
            int itemId)
        {
            var item = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci =>
                    ci.Id == itemId &&
                    ci.Cart.UserId == userId);

            if (item == null)
                return false;

            item.Quantity++;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DecreaseAsync(
            int userId,
            int itemId)
        {
            var item = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci =>
                    ci.Id == itemId &&
                    ci.Cart.UserId == userId);

            if (item == null)
                return false;

            if (item.Quantity > 1)
            {
                item.Quantity--;

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoveItemAsync(
            int userId,
            int itemId)
        {
            var item = await _context.CartItems
                .Include(ci => ci.Cart)
                .FirstOrDefaultAsync(ci =>
                    ci.Id == itemId &&
                    ci.Cart.UserId == userId);

            if (item == null)
                return false;

            _context.CartItems.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return false;

            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}