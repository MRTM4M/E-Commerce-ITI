using E_commerce_iti.Data;
using E_commerce_iti.Enum;
using E_commerce_iti.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class OrderService : IOrderService
    {
        private readonly  ECommerceDB _context;

        public OrderService(ECommerceDB context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            order.OrderDate = DateTime.Now;

            var productIds = order.Items
                .Select(i => i.ProductId)
                .Distinct()
                .ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            if (products.Count != productIds.Count)
                return false;

            foreach (var item in order.Items)
            {
                var product = products
                    .First(p => p.Id == item.ProductId);

                item.UnitPrice = product.Price;
            }

            order.TotalPrice = order.Items
                .Sum(i => i.UnitPrice * i.Quantity);

            _context.Orders.Add(order);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder == null)
                return false;

            existingOrder.ShippingCity = order.ShippingCity;
            existingOrder.ShippingStreet = order.ShippingStreet;
            existingOrder.ShippingCountry = order.ShippingCountry;
            existingOrder.Status = order.Status;

            // Remove old items
            _context.OrderItems.RemoveRange(existingOrder.Items);

            // Add new items
            var newItems = new List<OrderItem>();

            foreach (var item in order.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product == null)
                    return false;

                newItems.Add(new OrderItem
                {
                    OrderId = existingOrder.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            existingOrder.Items = newItems;

            existingOrder.TotalPrice = newItems
                .Sum(i => i.UnitPrice * i.Quantity);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return false;

            _context.OrderItems.RemoveRange(order.Items);
            _context.Orders.Remove(order);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return false;

            order.Status = status;

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}