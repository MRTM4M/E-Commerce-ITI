using E_commerce_iti.Enum;
using E_commerce_iti.Models;
using E_Commerce_iti.Models;

namespace E_commerce_iti.Data
{
    public static class Fakedata
    {
        // =========================
        // Users
        // =========================

        public static List<ApplicationUser> Users { get; set; } = new()
        {
            new ApplicationUser
            {
                Id = 1,
                FName = "Mahmoud",
                LName = "Helmy",
                Email = "mahmoud@example.com",
                UserName = "mahmoud",
                PhoneNumber = "01000000000",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },

            new ApplicationUser
            {
                Id = 2,
                FName = "Ahmed",
                LName = "Ali",
                Email = "ahmed@example.com",
                UserName = "ahmed",
                PhoneNumber = "01111111111",
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            }
        };


        // =========================
        // Addresses
        // =========================

        public static List<Address> Addresses { get; set; } = new()
        {
            new Address
            {
                Id = 1,
                UserId = 1,
                City = "Cairo",
                Country = "Egypt",
                Street = "Nasr City"
            },

            new Address
            {
                Id = 2,
                UserId = 2,
                City = "Giza",
                Country = "Egypt",
                Street = "Dokki"
            }
        };


        // =========================
        // Categories
        // =========================

        public static List<Category> Categories { get; set; } = new()
        {
            new Category
            {
                Id = 1,
                Name = "Laptops",
                Description = "Laptops and notebooks",
                ImageUrl = "laptops.jpg"
            },

            new Category
            {
                Id = 2,
                Name = "Phones",
                Description = "Smartphones and mobile devices",
                ImageUrl = "phones.jpg"
            },

            new Category
            {
                Id = 3,
                Name = "Accessories",
                Description = "Computer and phone accessories",
                ImageUrl = "accessories.jpg"
            }
        };


        // =========================
        // Products
        // =========================

        public static List<Product> Products { get; set; } = new()
        {
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Dell XPS 15",
                Price = 1500,
                Image = "dell-xps.jpg",
                Description = "High performance laptop",
                Stock = 10
            },

            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Lenovo ThinkPad",
                Price = 1200,
                Image = "thinkpad.jpg",
                Description = "Business laptop",
                Stock = 15
            },

            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "iPhone 15",
                Price = 900,
                Image = "iphone15.jpg",
                Description = "Apple smartphone",
                Stock = 20
            },

            new Product
            {
                Id = 4,
                CategoryId = 2,
                Name = "Samsung Galaxy S24",
                Price = 850,
                Image = "s24.jpg",
                Description = "Samsung flagship smartphone",
                Stock = 12
            },

            new Product
            {
                Id = 5,
                CategoryId = 3,
                Name = "Wireless Mouse",
                Price = 30,
                Image = "mouse.jpg",
                Description = "Wireless ergonomic mouse",
                Stock = 50
            }
        };


        // =========================
        // Carts
        // =========================

        public static List<Cart> Carts { get; set; } = new()
        {
            new Cart
            {
                Id = 1,
                UserId = 1
            },

            new Cart
            {
                Id = 2,
                UserId = 2
            }
        };


        // =========================
        // Cart Items
        // =========================

        public static List<CartItem> CartItems { get; set; } = new()
        {
            new CartItem
            {
                Id = 1,
                CartId = 1,
                ProductId = 1,
                Quantity = 2
            },

            new CartItem
            {
                Id = 2,
                CartId = 1,
                ProductId = 5,
                Quantity = 1
            },

            new CartItem
            {
                Id = 3,
                CartId = 2,
                ProductId = 3,
                Quantity = 1
            }
        };


        // =========================
        // Orders
        // =========================

        public static List<Order> Orders { get; set; } = new()
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                OrderDate = DateTime.UtcNow.AddDays(-5),
                TotalPrice = 3030,
                Status = OrderStatus.Pending,
                ShippingCity = "Cairo",
                ShippingStreet = "Nasr City",
                ShippingCountry = "Egypt"
            },

            new Order
            {
                Id = 2,
                UserId = 2,
                OrderDate = DateTime.UtcNow.AddDays(-2),
                TotalPrice = 900,
                Status = OrderStatus.Confirmed,
                ShippingCity = "Giza",
                ShippingStreet = "Dokki",
                ShippingCountry = "Egypt"
            }
        };


        // =========================
        // Order Items
        // =========================

        public static List<OrderItem> OrderItems { get; set; } = new()
        {
            new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                UnitPrice = 1500
            },

            new OrderItem
            {
                Id = 2,
                OrderId = 1,
                ProductId = 5,
                Quantity = 1,
                UnitPrice = 30
            },

            new OrderItem
            {
                Id = 3,
                OrderId = 2,
                ProductId = 3,
                Quantity = 1,
                UnitPrice = 900
            }
        };
    }
}