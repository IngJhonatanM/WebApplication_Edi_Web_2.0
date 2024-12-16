using EDIBANK.Conf_Db_With_Entity;
using EDIBANK.Models.Users_EdiWeb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EDIBANK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
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

            // Add Identity Setting for Username (Email) , Password Policy, Lock & Unloc User Accounts

            builder.Services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                //opts.SignIn.RequireConfirmedEmail = true;
                //opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = true;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 3;

            });

            // Add Setting for Identity the time of token validity
            // builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));

            // Add Identity Framework Core & Setting for Identity Cookie

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
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

            app.Use(async (context, next) =>
              {
                  try
                  {
                      await next(context);
                  }
                  catch (Exception e)
                  {
                      context.Response.StatusCode = 500;
                      context.Response.ContentType = "text/html";
                      context.Response.Redirect("/Home/Error");
                      /*              await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                                    await context.Response.WriteAsync("<h1>ERROR!</h1><br><br>\r\n");
                                   var exceptionHandlerPathFeature =
                               context.Features.Get<IExceptionHandlerPathFeature>();
                                   if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                                   {
                                       await context.Response.WriteAsync(
                                                                 "File error thrown!<br><br>\r\n");
                                   }

                                   await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                                   await context.Response.WriteAsync("</body></html>\r\n");
                                   await context.Response.WriteAsync(new string(' ', 512));*/

                  }

              });

            app.UseSession();
            app.UseAuthorization();

            app.MapRazorPages();

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