using E_commerce_iti.Data;
using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class CartService
    {
        private readonly ECommerceDBcontext _context;

        public CartService(ECommerceDBcontext context)
        {
            _context = context;
        }

        
        public async Task AddToCartAsync(CartItemViewModel model, string userId)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == model.ProductId && c.UserId == userId);

            if (existingItem != null)
            {
                existingItem.Quantity += model.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    UserId = userId
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
        }

       
        public async Task<List<CartItemViewModel>> GetUserCartItemsAsync(string userId)
        {
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .Select(c => new CartItemViewModel
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Price,
                    UserId = c.UserId
                })
                .ToListAsync();
        }

       
        public async Task<List<CartItemViewModel>> GetAllCartItemsForAdminAsync()
        {
            return await _context.CartItems
                .Select(c => new CartItemViewModel
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Price,
                    UserId = c.UserId
                })
                .ToListAsync();
        }

       
        public async Task UpdateQuantityAsync(int cartItemId, int newQuantity)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item != null && newQuantity > 0)
            {
                item.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }

       
        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}