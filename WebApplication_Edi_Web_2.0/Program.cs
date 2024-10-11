using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication_Edi_Web_2._0.Conf_Db_With_Entity;
using WebApplication_Edi_Web_2._0.Models.Users_EdiWeb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

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
            builder.Services.AddAuthorization();
            builder.Services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
            });

           // Add DbContext to represents a session with the database by Entity Framework Core.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Add Identity Framework Core & Setting for Identity "ApplicationUser"

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

            // Add Identity Setting for Username, Email, Password Policy

            builder.Services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                 //opts.SignIn.RequireConfirmedEmail = true;
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireLowercase = true;
            });

            // Add Identity Framework Core & Setting for Identity Cookie expiry time

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";  //set the login page.
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                
            });

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

           /* app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{area=Identity}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                 name: "pagehome",
                 pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();

            });*/
            /*app.MapControllerRoute(
                 name: "Bienvenida",
                pattern: "{area:Defaultlogin}/{controller=login}/{action=index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();

            app.Run();*/

             app.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");

             app.Run();
        }
    }
}