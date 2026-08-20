using System.ComponentModel.DataAnnotations;

namespace E_commerce_iti.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}