using Microsoft.AspNetCore.Identity;

namespace EDIBANK.Models.Users_EdiWeb
{
    public class ApplicationUser : IdentityUser
    {
        public string? DescripUser { get; set; }

        //  [DefaultValue("something")]
        public string? EDIId { get; set; }
    }
}