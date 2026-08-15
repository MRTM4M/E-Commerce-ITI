using E_commerce_iti.ViewModels;

namespace E_commerce_iti.Services
{
    public interface IOrderService
    {
        void CreateOrder(OrderCreateViewModel model);
        List<OrderDetailsViewModel> GetAllOrders();
        OrderDetailsViewModel? GetOrderById(int id);
        void UpdateOrderStatus(int id, string status);
        void DeleteOrder(int id);
    }
}