using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce_iti.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Street { get; set; }

        
        public ApplicationUser User { get; set; }
    }
}