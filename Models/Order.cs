using E_commerce_iti.Enum; 
using E_commerce_iti.Models;

public class Order
{
    public int Id { get; set; }
  
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingStreet { get; set; }
    public string ShippingCountry { get; set; }

    public ICollection<OrderItem> Items { get; set; } 
 
}