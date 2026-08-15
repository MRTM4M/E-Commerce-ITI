using E_Commerce_iti.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Data
{
    public class ECommerceDBcontext : DbContext
    {
        public ECommerceDBcontext(DbContextOptions<ECommerceDBcontext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Women" },
                new Category { Id = 2, Name = "Men" },
                new Category { Id = 3, Name = "Kids" },
                new Category { Id = 4, Name = "Sports" },
                new Category { Id = 5, Name = "Casual" },
                new Category { Id = 6, Name = "Heels" }
            );
        }
    }
}