
using E_commerce_iti.Models;

public class OrderItem
{
    public int Id { get; set; }           
    public int OrderId { get; set; }
 
    public int Quantity { get; set; }     
    public decimal UnitPrice { get; set; }
    public decimal Price => UnitPrice * Quantity;
    public int ProductId { get; set; }

    //Nav

    public Order Order {  get; set; }

}