using Microsoft.AspNetCore.Identity;
using System.Net;

namespace E_commerce_iti.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
