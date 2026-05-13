namespace House_Renting_System_Project.Services.Implementations;

using Contracts;
using House_renting_system_Project.Data.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Models.Auth;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IAuthService
{
    public async Task<bool> LoginAsync(LoginServiceModel model)
	{
		var user = await userManager.FindByEmailAsync(model.Email);
		if (user is null)
		{
			return false;
		}

		var isPasswordValid = await userManager.CheckPasswordAsync(
			user,
			model.Password);

		if (!isPasswordValid)
		{
			return false;
		}

		await signInManager.SignInAsync(
			user,
			model.RememberMe);

		return true;
	}

	public async Task<AuthOperationResult> RegisterAsync(
		RegisterServiceModel model)
	{
		var existingUser = await userManager.FindByEmailAsync(model.Email);
		if (existingUser is not null)
		{
			return new()
			{
				Errors = ["User already exists"]
			};
		}

		var newUser = new ApplicationUser
		{
			UserName = model.Username,
			Email = model.Email
		};

		var result = await userManager.CreateAsync(
			newUser,
			model.Password);


		return new()
		{
			Succeeded = result.Succeeded,
			Errors = result.Errors.Select(error => error.Description)
		};
	}

	public Task LogoutAsync()
		=> signInManager.SignOutAsync();
}
