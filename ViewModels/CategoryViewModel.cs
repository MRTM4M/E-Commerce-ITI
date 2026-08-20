using System.ComponentModel.DataAnnotations;

namespace E_commerce_iti.ViewModels
{
    public class CategoryViewModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}