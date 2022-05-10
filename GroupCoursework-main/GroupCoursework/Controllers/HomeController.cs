using GroupCoursework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GroupCoursework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        // To Excute Index to display all functionalites 
        public IActionResult Index()
        {
            return View();
        }


        // To Excute Index1 to display Crud
        public IActionResult Privacy()
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