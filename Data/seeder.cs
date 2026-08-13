
////DONOT DELETE THIS FILE, IT IS USED FOR SEEDING FAKE DATA INTO THE DATABASE WHEN THE
////APPLICATION IS RUN FOR THE FIRST TIME. IT IS COMMENTED OUT TO PREVENT ACCIDENTAL EXECUTION.
////UNCOMMENT AND CALL THE AddData METHOD IN Program.cs TO USE IT.
//using E_commerce_iti.Enum;
//using E_commerce_iti.Models;
//using Microsoft.EntityFrameworkCore;

//namespace E_commerce_iti.Data
//{
//    public static class seeder
//    {
//        public static void AddData(ECommerceDB context)
//        {
//            Console.WriteLine("Fake data seeding started...");

//            // =========================
//            // Remove Existing Data
//            // =========================

//            context.OrderItems.RemoveRange(context.OrderItems);
//            context.Orders.RemoveRange(context.Orders);

//            context.CartItems.RemoveRange(context.CartItems);
//            context.Carts.RemoveRange(context.Carts);

//            context.Products.RemoveRange(context.Products);
//            context.Categories.RemoveRange(context.Categories);

//            context.Addresses.RemoveRange(context.Addresses);

//            // Users
//            context.Users.RemoveRange(context.Users);

//            context.SaveChanges();

//            // =========================
//            // Reset Identity Seeds
//            // =========================

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('OrderItems', RESEED, 0)");

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('Orders', RESEED, 0)");

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('CartItems', RESEED, 0)");

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('Carts', RESEED, 0)");

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('Products', RESEED, 0)");

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('Categories', RESEED, 0)");

//            context.Database.ExecuteSqlRaw(
//                "DBCC CHECKIDENT ('Addresses', RESEED, 0)");

//            // =========================
//            // Users
//            // =========================

//            var users = new[]
//            {
//                new ApplicationUser
//                {
//                    Id = 1,
//                    FName = "Mahmoud",
//                    LName = "Helmy",
//                    UserName = "mahmoud",
//                    Email = "mahmoud@example.com",
//                    PhoneNumber = "01000000000",
//                    CreatedAt = DateTime.UtcNow.AddDays(-30)
//                },

//                new ApplicationUser
//                {
//                    Id = 2,
//                    FName = "Ahmed",
//                    LName = "Ali",
//                    UserName = "ahmed",
//                    Email = "ahmed@example.com",
//                    PhoneNumber = "01111111111",
//                    CreatedAt = DateTime.UtcNow.AddDays(-20)
//                },

//                new ApplicationUser
//                {
//                    Id = 3,
//                    FName = "Sara",
//                    LName = "Mohamed",
//                    UserName = "sara",
//                    Email = "sara@example.com",
//                    PhoneNumber = "01222222222",
//                    CreatedAt = DateTime.UtcNow.AddDays(-10)
//                }
//            };

//            context.Users.AddRange(users);

//            // =========================
//            // Addresses
//            // =========================

//            var addresses = new[]
//            {
//                new Address
//                {
//                    Id = 1,
//                    UserId = 1,
//                    City = "Cairo",
//                    Country = "Egypt",
//                    Street = "Nasr City"
//                },

//                new Address
//                {
//                    Id = 2,
//                    UserId = 1,
//                    City = "Cairo",
//                    Country = "Egypt",
//                    Street = "Heliopolis"
//                },

//                new Address
//                {
//                    Id = 3,
//                    UserId = 2,
//                    City = "Giza",
//                    Country = "Egypt",
//                    Street = "Dokki"
//                },

//                new Address
//                {
//                    Id = 4,
//                    UserId = 3,
//                    City = "Alexandria",
//                    Country = "Egypt",
//                    Street = "Smouha"
//                }
//            };

//            context.Addresses.AddRange(addresses);

//            // =========================
//            // Categories
//            // =========================

//            var categories = new[]
//            {
//                new Category
//                {
//                    Id = 1,
//                    Name = "Laptops",
//                    Description = "Laptops and notebooks",
//                    ImageUrl = "laptops.jpg"
//                },

//                new Category
//                {
//                    Id = 2,
//                    Name = "Phones",
//                    Description = "Smartphones and mobile devices",
//                    ImageUrl = "phones.jpg"
//                },

//                new Category
//                {
//                    Id = 3,
//                    Name = "Accessories",
//                    Description = "Computer and phone accessories",
//                    ImageUrl = "accessories.jpg"
//                }
//            };

//            context.Categories.AddRange(categories);

//            // =========================
//            // Products
//            // =========================

//            var products = new[]
//            {
//                new Product
//                {
//                    Id = 1,
//                    CategoryId = 1,
//                    Name = "Dell XPS 15",
//                    Price = 1500,
//                    Image = "dell-xps.jpg",
//                    Description = "High performance laptop",
//                    Stock = 10
//                },

//                new Product
//                {
//                    Id = 2,
//                    CategoryId = 1,
//                    Name = "Lenovo ThinkPad",
//                    Price = 1200,
//                    Image = "thinkpad.jpg",
//                    Description = "Business laptop",
//                    Stock = 15
//                },

//                new Product
//                {
//                    Id = 3,
//                    CategoryId = 2,
//                    Name = "iPhone 15",
//                    Price = 900,
//                    Image = "iphone15.jpg",
//                    Description = "Apple smartphone",
//                    Stock = 20
//                },

//                new Product
//                {
//                    Id = 4,
//                    CategoryId = 2,
//                    Name = "Samsung Galaxy S24",
//                    Price = 850,
//                    Image = "s24.jpg",
//                    Description = "Samsung flagship smartphone",
//                    Stock = 12
//                },

//                new Product
//                {
//                    Id = 5,
//                    CategoryId = 3,
//                    Name = "Wireless Mouse",
//                    Price = 30,
//                    Image = "mouse.jpg",
//                    Description = "Wireless ergonomic mouse",
//                    Stock = 50
//                },

//                new Product
//                {
//                    Id = 6,
//                    CategoryId = 3,
//                    Name = "Mechanical Keyboard",
//                    Price = 80,
//                    Image = "keyboard.jpg",
//                    Description = "Mechanical gaming keyboard",
//                    Stock = 25
//                }
//            };

//            context.Products.AddRange(products);

//            // =========================
//            // Carts
//            // =========================

//            var carts = new[]
//            {
//                new Cart
//                {
//                    Id = 1,
//                    UserId = 1
//                },

//                new Cart
//                {
//                    Id = 2,
//                    UserId = 2
//                },

//                new Cart
//                {
//                    Id = 3,
//                    UserId = 3
//                }
//            };

//            context.Carts.AddRange(carts);

//            // =========================
//            // Cart Items
//            // =========================

//            var cartItems = new[]
//            {
//                new CartItem
//                {
//                    Id = 1,
//                    CartId = 1,
//                    ProductId = 1,
//                    Quantity = 1
//                },

//                new CartItem
//                {
//                    Id = 2,
//                    CartId = 1,
//                    ProductId = 5,
//                    Quantity = 2
//                },

//                new CartItem
//                {
//                    Id = 3,
//                    CartId = 2,
//                    ProductId = 3,
//                    Quantity = 1
//                },

//                new CartItem
//                {
//                    Id = 4,
//                    CartId = 2,
//                    ProductId = 6,
//                    Quantity = 1
//                },

//                new CartItem
//                {
//                    Id = 5,
//                    CartId = 3,
//                    ProductId = 4,
//                    Quantity = 2
//                }
//            };

//            context.CartItems.AddRange(cartItems);

//            // =========================
//            // Orders
//            // =========================

//            var orders = new[]
//            {
//                new Order
//                {
//                    Id = 1,
//                    UserId = 1,
//                    OrderDate = DateTime.UtcNow.AddDays(-7),
//                    TotalPrice = 1560,
//                    Status = OrderStatus.Delivered,
//                    ShippingCity = "Cairo",
//                    ShippingStreet = "Nasr City",
//                    ShippingCountry = "Egypt"
//                },

//                new Order
//                {
//                    Id = 2,
//                    UserId = 1,
//                    OrderDate = DateTime.UtcNow.AddDays(-2),
//                    TotalPrice = 900,
//                    Status = OrderStatus.Pending,
//                    ShippingCity = "Cairo",
//                    ShippingStreet = "Heliopolis",
//                    ShippingCountry = "Egypt"
//                },

//                new Order
//                {
//                    Id = 3,
//                    UserId = 2,
//                    OrderDate = DateTime.UtcNow.AddDays(-4),
//                    TotalPrice = 980,
//                    Status = OrderStatus.Shipped,
//                    ShippingCity = "Giza",
//                    ShippingStreet = "Dokki",
//                    ShippingCountry = "Egypt"
//                },

//                new Order
//                {
//                    Id = 4,
//                    UserId = 3,
//                    OrderDate = DateTime.UtcNow.AddDays(-1),
//                    TotalPrice = 1700,
//                    Status = OrderStatus.Confirmed,
//                    ShippingCity = "Alexandria",
//                    ShippingStreet = "Smouha",
//                    ShippingCountry = "Egypt"
//                }
//            };

//            context.Orders.AddRange(orders);

//            // =========================
//            // Order Items
//            // =========================

//            var orderItems = new[]
//            {
//                new OrderItem
//                {
//                    Id = 1,
//                    OrderId = 1,
//                    ProductId = 1,
//                    Quantity = 1,
//                    UnitPrice = 1500
//                },

//                new OrderItem
//                {
//                    Id = 2,
//                    OrderId = 1,
//                    ProductId = 5,
//                    Quantity = 2,
//                    UnitPrice = 30
//                },

//                new OrderItem
//                {
//                    Id = 3,
//                    OrderId = 2,
//                    ProductId = 3,
//                    Quantity = 1,
//                    UnitPrice = 900
//                },

//                new OrderItem
//                {
//                    Id = 4,
//                    OrderId = 3,
//                    ProductId = 3,
//                    Quantity = 1,
//                    UnitPrice = 900
//                },

//                new OrderItem
//                {
//                    Id = 5,
//                    OrderId = 3,
//                    ProductId = 5,
//                    Quantity = 2,
//                    UnitPrice = 30
//                },

//                new OrderItem
//                {
//                    Id = 6,
//                    OrderId = 4,
//                    ProductId = 4,
//                    Quantity = 2,
//                    UnitPrice = 850
//                }
//            };

//            context.OrderItems.AddRange(orderItems);

//            // =========================
//            // Save Everything
//            // =========================

//            context.SaveChanges();

//            Console.WriteLine("Fake data added successfully!");
//        }
//    }
//}