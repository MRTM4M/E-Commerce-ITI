using E_commerce_iti.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Data
{
    public class ECommerceDBcontext : DbContext
    {
        public ECommerceDBcontext(DbContextOptions<ECommerceDBcontext> options)
            : base(options)
        {
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}