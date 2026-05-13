namespace House_renting_system_Project.Data.Data.Entities;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
	public List<House> Houses { get; set; } = null!;
}
