using System.ComponentModel.DataAnnotations;

namespace House_renting_system_Project.Models.Auth
{
	public class LoginViewModel
	{
		[Required]
		[StringLength(20, MinimumLength = 3, ErrorMessage = "Invalid Username")]
		public string Username { get; set; } = string.Empty;

		[Required]
		[StringLength(80, MinimumLength = 6, ErrorMessage = "Invalid Password")]
		public string Password { get; set; } = string.Empty;

		public bool RememberMe { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
	}
}
