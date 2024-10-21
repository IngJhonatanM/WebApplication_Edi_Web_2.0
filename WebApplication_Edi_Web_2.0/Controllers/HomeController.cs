using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication_Edi_Web_2._0.Models;

namespace WebApplication_Edi_Web_2._0.Controllers
{

   // [Authorize(Roles = "User")]
    public class HomeController : Controller

        
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Auth_Users to back View

        [Authorize]
        public IActionResult Secured()
        {
            return View((object)"Hello");
        }
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Salida()

        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}