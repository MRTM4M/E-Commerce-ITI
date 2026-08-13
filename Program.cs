using E_commerce_iti.Data;
using E_commerce_iti.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti
{
    public class Program
    {
        public static async Task AddRoles(WebApplication app){
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole<int>>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("Customer"))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>("Customer"));
                }
            }
        }
        public static async Task AddData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider
                    .GetRequiredService<ECommerceDB>();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                await Seeder.AddData(context, userManager);
            }
        }

        public static async Task MakeAllUsersCustomers(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                var users = await userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    if (!await userManager.IsInRoleAsync(user, "Customer"))
                    {
                        await userManager.AddToRoleAsync(user, "Customer");
                    }
                }
            }
        }

        public static async Task MakeUserAdmin(WebApplication app,string email)
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    Console.WriteLine($"User with email {email} was not found.");
                    return;
                }

                if (!await userManager.IsInRoleAsync(user, "Admin"))
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                Console.WriteLine($"{email} is now an Admin.");
            }
        }
        public static async Task Main(string[] args)
            {
            var builder = WebApplication.CreateBuilder(args);

            //connection string for database on hold, uncomment the following lines to use it

            builder.Services.AddDbContext<ECommerceDB>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    )
            );
            //adding identity services to the project +
            //default identity configuration for ApplicationUser
            //and IdentityRole<int> with EntityFramework stores + its ui
            builder.Services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<ECommerceDB>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //add defult services for razor pages
            builder.Services.AddRazorPages();

            var app = builder.Build();
            await AddRoles(app);
            await AddData(app);
            await MakeAllUsersCustomers(app);
            await MakeUserAdmin(app, "mahmoud@example.com");
            // Seed the database with initial data

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();



            app.Run();
        }
    }
}
