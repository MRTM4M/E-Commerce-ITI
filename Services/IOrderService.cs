using E_commerce_iti.Enum;
using E_commerce_iti.Models;

namespace E_commerce_iti.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetUserOrdersAsync(int userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<bool> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status);
    }
}