using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Edi_Web_2._0.Models.Users_EdiWeb
{
    public class ApplicationUser : IdentityUser
    {
        public string? DescripUser { get; set; }

       

    }
}