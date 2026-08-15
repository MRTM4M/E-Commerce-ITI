using Microsoft.EntityFrameworkCore;
using E_commerce_iti.Models;

namespace E_commerce_iti.Data
{
    public class ECommerceDBcontext : DbContext
    {
        public ECommerceDBcontext(
            DbContextOptions<ECommerceDBcontext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}