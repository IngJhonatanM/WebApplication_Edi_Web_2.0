using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication_Edi_Web_2._0.Conf_Db_With_Entity;
using WebApplication_Edi_Web_2._0.Models.Users_EdiWeb;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication_Edi_Web_2._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();


            // Add DbContext to represents a session with the database by Entity Framework Core.

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Add Identity Framework Core & Setting for "ApplicationUser"

            builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            //.AddRoles<IdentityRole>()

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages ();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}