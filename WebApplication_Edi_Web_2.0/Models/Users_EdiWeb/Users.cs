using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Edi_Web_2._0.Models.Users_EdiWeb
{
    public class Users
    {
            [Required]
            public string Name { get; set; }

            [Required]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
            public string? DescripUser { get; set; }

    }
    }