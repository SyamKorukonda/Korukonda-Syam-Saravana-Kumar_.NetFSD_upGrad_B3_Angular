using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;
using WebApplication6.Repositories;
using WebApplication6.Services;

namespace WebApplication6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Register DbContext in Program.cs
            builder.Services.AddDbContext<ApplicationDbContext>(options =>

            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Service Registration 
           builder.Services.AddTransient<IProductRepository, ProductRepository>();
           builder.Services.AddTransient<IProductService, ProductService>();    


            var app = builder.Build();

           
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
