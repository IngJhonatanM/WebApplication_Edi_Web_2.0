using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication_Edi_Web_2._0.Models.Users_EdiWeb;


// This class "ApplicationDbContext" Connect to the database using "Entity Framework"

namespace WebApplication_Edi_Web_2._0.Conf_Db_With_Entity
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        // Using dbcontext to assign roles

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var Admin = new IdentityRole("Admin");
            Admin.NormalizedName = "Admin";

            var User = new IdentityRole("User");
            User.NormalizedName = "User";


            builder.Entity<IdentityRole>().HasData(Admin, User);

        }




    }
}

// This class "ApplicationDbContext" will be a service so that it can be used by other classes. This class "ApplicationDbContext" extends "Entity Framework"