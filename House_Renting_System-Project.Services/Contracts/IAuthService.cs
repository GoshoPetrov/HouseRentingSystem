namespace House_Renting_System_Project.Services.Contracts;

using Models.Auth;
public interface IAuthService
{
	Task<bool> LoginAsync(LoginServiceModel model);

	Task<AuthOperationResult> RegisterAsync(
		RegisterServiceModel model);

	Task LogoutAsync();
}
