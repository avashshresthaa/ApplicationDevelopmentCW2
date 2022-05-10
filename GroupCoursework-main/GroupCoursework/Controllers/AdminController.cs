using GroupCoursework.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupCoursework.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;        }
        public IActionResult Index()
        {
            return View();
        }


        // To create the admin role that will be assigned to the new user
        public async Task<IActionResult> CreateRoleAdmin()
        {
          
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",

                };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }


    

            return RedirectToAction("Index", "Home");
        }


        // To create the staff role that will be assigned to the new user
        public async Task<IActionResult> CreateRoleStaff()
        {
          
            IdentityRole role = new IdentityRole
            {
                Name = "Staff",

            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }


        

            return RedirectToAction("Index", "Home");
        }
    }
}
