namespace House_renting_system_Project.Controllers;

using House_Renting_System_Project.Services.Contracts;
using House_Renting_System_Project.Services.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
    
public class AuthController(IAuthService service) : Controller
{
	[HttpGet]
	public IActionResult Login()
		=> this.View();

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(model);
		}

		var loginServiceModel = new LoginServiceModel
		{
			Email = model.Email,
			Password = model.Password,
			RememberMe = model.RememberMe
		};

        var isLoggedIn = await service.LoginAsync(loginServiceModel);

		if (isLoggedIn)
		{
			return this.RedirectToAction("Index", "Home");
		}

		this.ModelState.AddModelError(
			string.Empty,
			"Invalid login attempt.");

		return this.View(model);
	}

	[HttpGet]
	public IActionResult Register()
		=> this.View();

	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(model);
		}

		var registerServiceModel = new RegisterServiceModel
		{
			Username = model.Username,
			Email = model.Email,
			Password = model.Password,
		};

        var result = await service.RegisterAsync(registerServiceModel);

		if (result.Succeeded)
		{
			this.TempData["Success"] = "User Created Successfully!";
			return this.RedirectToAction(nameof(this.Login));
		}

		foreach (var error in result.Errors)
		{
			this.ModelState.AddModelError("", error);
		}

		return this.View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Logout()
	{
		await service.LogoutAsync();
		return this.RedirectToAction("Index", "Home");
	}
}
