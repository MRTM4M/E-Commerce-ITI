using System.Collections.Generic;

namespace E_commerce_iti.ViewModels
{
    public class ShopViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();

        public List<CategoryViewModel> Categories { get; set; } = new();

        public string? Search { get; set; }

        public int? CategoryId { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}