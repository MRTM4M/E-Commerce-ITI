namespace E_commerce_iti.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

       
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}