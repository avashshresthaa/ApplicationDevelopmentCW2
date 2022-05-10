using GroupCoursework.Models;
using GroupCoursework.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupCoursework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //Only Admin can access 
        [Authorize(Roles = "Admin")]

        //To display RegisterStaff
        public IActionResult RegisterStaff()
        {
            return View();
        }
        //To display RegisterAdmin
        public IActionResult RegisterAdmin()
        {
            return View();
        }

      
        [HttpPost]
        //Only Admin can access this functio
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterStaff(RegisterModel registermodel)
        {
            //Checing the validation of the model
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registermodel.Email,
                };

                var result = await _userManager.CreateAsync(user, registermodel.Password);
                var RegisteredUser = user.Id;
                if (result.Succeeded)
                {
                    var userRole = new IdentityRole();
                 result = await _userManager.AddToRoleAsync(user, "Staff");
                    Console.WriteLine(result);
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return RedirectToAction("RegisterStaff");
        }

        //POST Method For Admin Registration
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterModel registermodel)
        {
            //Checing the validation of the model
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registermodel.Email,
                };

                var result = await _userManager.CreateAsync(user, registermodel.Password);
                var RegisteredUser = user.Id;
                if (result.Succeeded)
                {
                    var userRole = new IdentityRole();
                    result = await _userManager.AddToRoleAsync(user, "Admin");
                    Console.WriteLine(result);
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(registermodel);
        }



        //GetMethod
        [HttpGet]
        [AllowAnonymous]

        //To Display Login view
        public IActionResult Login()
        {
            return View();
        }

        //Post Method
        [HttpPost]
        [AllowAnonymous]

        //Log in function to take user input and redirect according to the result
        public async Task<IActionResult> Login(LoginModel loginmodel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginmodel.Email, loginmodel.Password, !loginmodel.RememberMe, false);//if lock account on failure
                //returnns sign in result
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError(string.Empty, "Invalid Username/Password");

            }
            return View();
        }

        //To create log out function of the system 
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        //For the function 14 of the coursework

        [HttpGet]
        [Authorize]
        public IActionResult ResetPassword()
        {

            return View();
        }

        //Password reset function to change the password
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                // To get the detail of the users
                var user = await _userManager.GetUserAsync(User); 
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                //Function to change the password of the user

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)

                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);
                return View("ResetPasswordConfirmation");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            ViewBag.users = users;
            return View();
        }
        
           public async Task<IActionResult> ResetUserPassword(string email, string password)
        {
            bool suc = false;
            var user = await _userManager.FindByNameAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var userPassword = await _userManager.ResetPasswordAsync(user, token, password);

            return RedirectToAction("ListUsers", new { email = email, IsSuccess = true });
        }

    }
}
