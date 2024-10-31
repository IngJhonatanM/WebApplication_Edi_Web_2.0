using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_Edi_Web_2._0.Models.Users_EdiWeb
{
    public class Users
    {
            [Required(ErrorMessage = "Por favor, ingresar nombre. ")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Por favor, ingresar email. ")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Por favor, ingresar Contraseña. ")]
            public string Password { get; set; }
            public string? DescripUser { get; set; }

    }
    }