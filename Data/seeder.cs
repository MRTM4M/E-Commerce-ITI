using E_commerce_iti.Enum;
using E_commerce_iti.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//DONOT DELETE THIS FILE, IT IS USED FOR SEEDING FAKE DATA INTO THE DATABASE WHEN THE
//APPLICATION IS RUN FOR THE FIRST TIME. IT IS COMMENTED OUT TO PREVENT ACCIDENTAL EXECUTION.
//UNCOMMENT AND CALL THE AddData METHOD IN Program.cs TO USE IT.
namespace E_commerce_iti.Data
{
    public static class Seeder
    {
        public static async Task AddData(
            ECommerceDB context,
            UserManager<ApplicationUser> userManager)
        {
            Console.WriteLine("Fake data seeding started...");

            // =========================
            // Remove Existing Data
            // =========================

            context.OrderItems.RemoveRange(context.OrderItems);
            context.Orders.RemoveRange(context.Orders);

            context.CartItems.RemoveRange(context.CartItems);
            context.Carts.RemoveRange(context.Carts);

            context.Products.RemoveRange(context.Products);
            context.Categories.RemoveRange(context.Categories);

            context.Addresses.RemoveRange(context.Addresses);

            await context.SaveChangesAsync();

            // =========================
            // Users
            // =========================

            var existingUsers = await context.Users.ToListAsync();

            foreach (var user in existingUsers)
            {
                await userManager.DeleteAsync(user);
            }

            var mahmoud = new ApplicationUser
            {
                FName = "Mahmoud",
                LName = "Helmy",
                UserName = "mahmoud@example.com",
                Email = "mahmoud@example.com",
                PhoneNumber = "01000000000",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            };

            var result = await userManager.CreateAsync(
                mahmoud,
                "Mahmoud123!"
            );

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            await userManager.AddToRoleAsync(mahmoud, "Customer");

            var ahmed = new ApplicationUser
            {
                FName = "Ahmed",
                LName = "Ali",
                UserName = "ahmed@example.com",
                Email = "ahmed@example.com",
                PhoneNumber = "01111111111",
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            };

            result = await userManager.CreateAsync(
                ahmed,
                "Ahmed123!"
            );

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            await userManager.AddToRoleAsync(ahmed, "Customer");

            var sara = new ApplicationUser
            {
                FName = "Sara",
                LName = "Mohamed",
                UserName = "sara@example.com",
                Email = "sara@example.com",
                PhoneNumber = "01222222222",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            result = await userManager.CreateAsync(
                sara,
                "Sara123!"
            );

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            await userManager.AddToRoleAsync(sara, "Customer");

            // =========================
            // Addresses
            // =========================

            var addresses = new[]
            {
                new Address
                {
                    UserId = mahmoud.Id,
                    City = "Cairo",
                    Country = "Egypt",
                    Street = "Nasr City"
                },

                new Address
                {
                    UserId = mahmoud.Id,
                    City = "Cairo",
                    Country = "Egypt",
                    Street = "Heliopolis"
                },

                new Address
                {
                    UserId = ahmed.Id,
                    City = "Giza",
                    Country = "Egypt",
                    Street = "Dokki"
                },

                new Address
                {
                    UserId = sara.Id,
                    City = "Alexandria",
                    Country = "Egypt",
                    Street = "Smouha"
                }
            };

            context.Addresses.AddRange(addresses);

            // =========================
            // Categories
            // =========================

            var categories = new[]
            {
                new Category
                {
                    Name = "Women",
                    Description = "Women's shoes and sneakers",
                    ImageUrl = "women.jpg"
                },

                new Category
                {
                    Name = "Men",
                    Description = "Men's shoes and sneakers",
                    ImageUrl = "men.jpg"
                },

                new Category
                {
                    Name = "Kids",
                    Description = "Shoes and sneakers for kids",
                    ImageUrl = "kids.jpg"
                },

                new Category
                {
                    Name = "Sports",
                    Description = "Sports and running shoes",
                    ImageUrl = "sports.jpg"
                },

                new Category
                {
                    Name = "Casual",
                    Description = "Casual shoes for everyday use",
                    ImageUrl = "casual.jpg"
                },

                new Category
                {
                    Name = "Heels",
                    Description = "Women's heels and elegant shoes",
                    ImageUrl = "heels.jpg"
                }
            };

            context.Categories.AddRange(categories);

            await context.SaveChangesAsync();


            // =========================
            // Products
            // =========================

            var products = new[]
            {
                // =========================
                // Women
                // =========================

                new Product
                {
                    CategoryId = categories[0].Id,
                    Name = "Women's White Sneakers",
                    Price = 75,
                    Image = "women-white-sneakers.jpg",
                    Description = "Comfortable white sneakers for women",
                    Stock = 20
                },

                new Product
                {
                    CategoryId = categories[0].Id,
                    Name = "Women's Running Shoes",
                    Price = 90,
                    Image = "women-running-shoes.jpg",
                    Description = "Lightweight running shoes for women",
                    Stock = 15
                },


                // =========================
                // Men
                // =========================

                new Product
                {
                    CategoryId = categories[1].Id,
                    Name = "Men's Classic Sneakers",
                    Price = 85,
                    Image = "men-classic-sneakers.jpg",
                    Description = "Classic sneakers for everyday wear",
                    Stock = 25
                },

                new Product
                {
                    CategoryId = categories[1].Id,
                    Name = "Men's Leather Shoes",
                    Price = 120,
                    Image = "men-leather-shoes.jpg",
                    Description = "Premium leather shoes for men",
                    Stock = 10
                },


                // =========================
                // Kids
                // =========================

                new Product
                {
                    CategoryId = categories[2].Id,
                    Name = "Kids Colorful Sneakers",
                    Price = 45,
                    Image = "kids-colorful-sneakers.jpg",
                    Description = "Colorful and comfortable sneakers for kids",
                    Stock = 30
                },

                new Product
                {
                    CategoryId = categories[2].Id,
                    Name = "Kids Running Shoes",
                    Price = 50,
                    Image = "kids-running-shoes.jpg",
                    Description = "Lightweight running shoes for kids",
                    Stock = 20
                },


                // =========================
                // Sports
                // =========================

                new Product
                {
                    CategoryId = categories[3].Id,
                    Name = "Pro Running Shoes",
                    Price = 110,
                    Image = "pro-running-shoes.jpg",
                    Description = "High performance shoes for running and training",
                    Stock = 18
                },

                new Product
                {
                    CategoryId = categories[3].Id,
                    Name = "Training Sneakers",
                    Price = 95,
                    Image = "training-sneakers.jpg",
                    Description = "Durable sneakers for sports and training",
                    Stock = 22
                },


                // =========================
                // Casual
                // =========================

                new Product
                {
                    CategoryId = categories[4].Id,
                    Name = "Classic Casual Sneakers",
                    Price = 70,
                    Image = "classic-casual.jpg",
                    Description = "Comfortable casual sneakers for everyday use",
                    Stock = 25
                },

                new Product
                {
                    CategoryId = categories[4].Id,
                    Name = "Canvas Casual Shoes",
                    Price = 60,
                    Image = "canvas-casual.jpg",
                    Description = "Lightweight canvas shoes for everyday wear",
                    Stock = 30
                },


                // =========================
                // Heels
                // =========================

                new Product
                {
                    CategoryId = categories[5].Id,
                    Name = "Classic Black Heels",
                    Price = 100,
                    Image = "black-heels.jpg",
                    Description = "Elegant black heels for special occasions",
                    Stock = 12
                },

                new Product
                {
                    CategoryId = categories[5].Id,
                    Name = "Elegant Red Heels",
                    Price = 115,
                    Image = "red-heels.jpg",
                    Description = "Elegant red heels with a stylish design",
                    Stock = 8
                }
            };

            context.Products.AddRange(products);

            await context.SaveChangesAsync();

            // =========================
            // Carts
            // =========================

            var carts = new[]
            {
                new Cart
                {
                    UserId = mahmoud.Id
                },

                new Cart
                {
                    UserId = ahmed.Id
                },

                new Cart
                {
                    UserId = sara.Id
                }
            };

            context.Carts.AddRange(carts);

            await context.SaveChangesAsync();

            // =========================
            // Cart Items
            // =========================

            var cartItems = new[]
            {
                new CartItem
                {
                    CartId = carts[0].Id,
                    ProductId = products[0].Id,
                    Quantity = 1
                },

                new CartItem
                {
                    CartId = carts[0].Id,
                    ProductId = products[4].Id,
                    Quantity = 2
                },

                new CartItem
                {
                    CartId = carts[1].Id,
                    ProductId = products[2].Id,
                    Quantity = 1
                },

                new CartItem
                {
                    CartId = carts[1].Id,
                    ProductId = products[5].Id,
                    Quantity = 1
                },

                new CartItem
                {
                    CartId = carts[2].Id,
                    ProductId = products[3].Id,
                    Quantity = 2
                }
            };

            context.CartItems.AddRange(cartItems);

            // =========================
            // Orders
            // =========================

            var orders = new[]
            {
                new Order
                {
                    UserId = mahmoud.Id,
                    OrderDate = DateTime.UtcNow.AddDays(-7),
                    TotalPrice = 1560,
                    Status = OrderStatus.Delivered,
                    ShippingCity = "Cairo",
                    ShippingStreet = "Nasr City",
                    ShippingCountry = "Egypt"
                },

                new Order
                {
                    UserId = mahmoud.Id,
                    OrderDate = DateTime.UtcNow.AddDays(-2),
                    TotalPrice = 900,
                    Status = OrderStatus.Pending,
                    ShippingCity = "Cairo",
                    ShippingStreet = "Heliopolis",
                    ShippingCountry = "Egypt"
                },

                new Order
                {
                    UserId = ahmed.Id,
                    OrderDate = DateTime.UtcNow.AddDays(-4),
                    TotalPrice = 960,
                    Status = OrderStatus.Shipped,
                    ShippingCity = "Giza",
                    ShippingStreet = "Dokki",
                    ShippingCountry = "Egypt"
                },

                new Order
                {
                    UserId = sara.Id,
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    TotalPrice = 1700,
                    Status = OrderStatus.Confirmed,
                    ShippingCity = "Alexandria",
                    ShippingStreet = "Smouha",
                    ShippingCountry = "Egypt"
                }
            };

            context.Orders.AddRange(orders);

            await context.SaveChangesAsync();

            // =========================
            // Order Items
            // =========================

            var orderItems = new[]
            {
                new OrderItem
                {
                    OrderId = orders[0].Id,
                    ProductId = products[0].Id,
                    Quantity = 1,
                    UnitPrice = 1500
                },

                new OrderItem
                {
                    OrderId = orders[0].Id,
                    ProductId = products[4].Id,
                    Quantity = 2,
                    UnitPrice = 30
                },

                new OrderItem
                {
                    OrderId = orders[1].Id,
                    ProductId = products[2].Id,
                    Quantity = 1,
                    UnitPrice = 900
                },

                new OrderItem
                {
                    OrderId = orders[2].Id,
                    ProductId = products[2].Id,
                    Quantity = 1,
                    UnitPrice = 900
                },

                new OrderItem
                {
                    OrderId = orders[2].Id,
                    ProductId = products[4].Id,
                    Quantity = 2,
                    UnitPrice = 30
                },

                new OrderItem
                {
                    OrderId = orders[3].Id,
                    ProductId = products[3].Id,
                    Quantity = 2,
                    UnitPrice = 850
                }
            };

            context.OrderItems.AddRange(orderItems);

            await context.SaveChangesAsync();

            Console.WriteLine("Fake data added successfully!");
        }
    }
}