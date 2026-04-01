using HouseRentingSystem.Models;
using HouseRentingSystemData.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid == false)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return View(model);
            }

            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (result)
            {
                await signInManager.SignInAsync(user,model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {
            if(ModelState.IsValid == false)
            {
                return View(model);
            }
            var user = userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                return View(model);
            }

            var newUser = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = userManager.CreateAsync(newUser,model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Descrition);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
