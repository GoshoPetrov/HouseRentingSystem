namespace House_Renting_System_Project.Services.Models.Auth;

public class AuthOperationResult
{
	public bool Succeeded { get; init; }

	public IEnumerable<string> Errors { get; init; } = [];
}
