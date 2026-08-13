using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce_iti.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ApplicationUser User { get; set; }
    }
}
