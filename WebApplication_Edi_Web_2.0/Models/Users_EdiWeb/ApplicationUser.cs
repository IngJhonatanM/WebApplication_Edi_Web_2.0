using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace WebApplication_Edi_Web_2._0.Models.Users_EdiWeb
{
    public class ApplicationUser : IdentityUser
    {
        public string? DescripUser { get; set; }

      //  [DefaultValue("something")]
        public string? EDIId { get; set; }
    }
}