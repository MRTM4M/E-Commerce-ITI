namespace E_commerce_iti.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new();
    }

    public class CartItemViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}