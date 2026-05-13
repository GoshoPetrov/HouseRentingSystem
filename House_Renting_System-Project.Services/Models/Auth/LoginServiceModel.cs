namespace House_Renting_System_Project.Services.Models.Auth;

public class LoginServiceModel
{
	public string Email { get; set; } = "";

	public string Password { get; set; } = "";

	public bool RememberMe { get; set; }
}
