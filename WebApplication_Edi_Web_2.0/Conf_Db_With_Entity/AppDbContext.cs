using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication_Edi_Web_2._0.Models.Users_EdiWeb;

/* This class "AppDbContext" extends "Entity Framework" represents a session with the database
// and can be used to our custom in the context so that in the migration the Framework user knows to make changes to the schema.*/

namespace WebApplication_Edi_Web_2._0.Conf_Db_With_Entity
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
             
        }

        // Using dbcontext to create roles

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