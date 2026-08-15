using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using E_commerce_iti.Enum; 
using E_commerce_iti.Data; 
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceDBcontext _context;

        public OrderService(ECommerceDBcontext context)
        {
            _context = context;
        }

        public void CreateOrder(OrderCreateViewModel model)
        {
            var order = new Order
            {
                ShippingCity = model.ShippingCity,
                ShippingStreet = model.ShippingStreet,
                ShippingCountry = model.ShippingCountry,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending 
            };

            decimal total = 0;

            foreach (var itemVm in model.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = itemVm.ProductId,
                    Quantity = itemVm.Quantity,
                    UnitPrice = itemVm.UnitPrice
                };

                total += orderItem.Price;
                order.Items.Add(orderItem);
            }

            order.TotalPrice = total;

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<OrderDetailsViewModel> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.Items)
                .Select(o => new OrderDetailsViewModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice,
                    Status = o.Status.ToString(), 
                    ShippingCity = o.ShippingCity,
                    ShippingStreet = o.ShippingStreet,
                    ShippingCountry = o.ShippingCountry,
                    Items = o.Items.Select(i => new OrderItemDetailsViewModel
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Price = i.Price
                    }).ToList()
                }).ToList();
        }

        public OrderDetailsViewModel? GetOrderById(int id)
        {
            var o = _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(x => x.Id == id);

            if (o == null) return null;

            return new OrderDetailsViewModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status.ToString(),
                ShippingCity = o.ShippingCity,
                ShippingStreet = o.ShippingStreet,
                ShippingCountry = o.ShippingCountry,
                Items = o.Items.Select(i => new OrderItemDetailsViewModel
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Price = i.Price
                }).ToList()
            };
        }

        public void UpdateOrderStatus(int id, string status)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                if (System.Enum.TryParse<OrderStatus>(status, true, out var parsedStatus))
                {
                    order.Status = parsedStatus;
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Include(o => o.Items).FirstOrDefault(x => x.Id == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}