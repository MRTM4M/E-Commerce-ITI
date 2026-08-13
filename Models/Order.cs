using E_commerce_iti.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce_iti.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        [StringLength(100)]
        public string ShippingCity { get; set; }

        [Required]
        [StringLength(200)]
        public string ShippingStreet { get; set; }

        [Required]
        [StringLength(100)]
        public string ShippingCountry { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public ApplicationUser User { get; set; }
    }
}