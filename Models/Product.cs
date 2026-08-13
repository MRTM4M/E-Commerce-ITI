using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_iti.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
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

        public Category Category { get; set; }
    }
}