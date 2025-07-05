using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcITIProject.Models;
using MvcITIProject.Repositories;
using MvcITIProject.UnitOfWorks;

namespace MvcITIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Add Connectionstring and UnitOfWork

            builder.Services.AddDbContext<LibraryContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("emaddb")).UseLazyLoadingProxies());


            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<AuthorRepository>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<LibraryContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.0.
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

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
