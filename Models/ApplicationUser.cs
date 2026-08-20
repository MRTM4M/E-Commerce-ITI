using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_commerce_iti.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}