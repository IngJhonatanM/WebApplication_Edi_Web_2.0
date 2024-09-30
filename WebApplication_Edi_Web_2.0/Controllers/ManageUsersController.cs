using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Edi_Web_2._0.Models.Users_EdiWeb;

namespace WebApplication_Edi_Web_2._0.Controllers
{

   // Instance of the ASP.NET Core Identity UserManager available in the controller
    //[Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser>
            _userManager;

        public ManageUsersController(UserManager<ApplicationUser> usrMgr)
        {
            _userManager = usrMgr;
        }



        //use the GetUserAsync method to retrieve an instance of ApplicationIdentityUser for the currently logged in user.
        //This means making the controller method asynchronous so that we can await the call to GetUserAsync 
        public async Task<IActionResult> Index()
        {
            var admin = (await _userManager
                .GetUsersInRoleAsync("Admin"))
                .ToArray();

            var everyone = await _userManager.Users
                .ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Admin = admin,
                Everyone = everyone
            };

            return View(model);
        }

        // Create User

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }





    }
}
