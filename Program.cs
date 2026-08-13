using E_commerce_iti.Data;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti
{
    public class Program
    {

    public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // connection string for database on hold, uncomment the following lines to use it
            //builder.Services.AddDbContext<ECommerceDB>(options =>
            //    options.UseSqlServer(
            //        builder.Configuration.GetConnectionString("DefaultConnection")
            //        )
            //);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Seed the database with initial data
            //using (var scope = app.Services.CreateScope())
            //{
            //    var context =
            //        scope.ServiceProvider.GetRequiredService<ECommerceDB>();

            //    seeder.AddData(context);
            //}
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();



            app.Run();
        }
    }
}
