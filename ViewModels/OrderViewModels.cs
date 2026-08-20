using E_commerce_iti.Enum;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_iti.ViewModels
{
    public class OrderIndexViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingCity { get; set; }
        public int ItemsCount { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCountry { get; set; }
        public int UserId { get; set; }
        public List<OrderItemDetailsViewModel> Items { get; set; } = new();
    }

    public class OrderItemDetailsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }


    public class OrderFormViewModel
        {
        public int Id { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required, StringLength(100)]
        public string ShippingCity { get; set; }

        [Required, StringLength(200)]
        public string ShippingStreet { get; set; }

        [Required, StringLength(100)]
        public string ShippingCountry { get; set; }

        public List<OrderItemFormViewModel> Items { get; set; } = new();

        public List<ProductOptionViewModel> AvailableProducts { get; set; } = new();
    }

    public class OrderItemFormViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }

    public class ProductOptionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ChangeOrderStatusViewModel
    {
        public int Id { get; set; }
        public OrderStatus CurrentStatus { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
    }
}