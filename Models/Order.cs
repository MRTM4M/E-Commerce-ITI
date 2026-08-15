using E_commerce_iti.Enum;

namespace E_commerce_iti.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public string ShippingCity { get; set; } = null!;

        public string ShippingStreet { get; set; } = null!;

        public string ShippingCountry { get; set; } = null!;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}