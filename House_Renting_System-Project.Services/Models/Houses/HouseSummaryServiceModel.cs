namespace House_Renting_System_Project.Services.Models.Houses;

public class HouseSummaryServiceModel
{
	public int Id { get; set; }

	public string Name { get; set; } = "";

	public string Address { get; set; } = "";

	public string ImageUrl { get; set; } = "";

	public bool CurrentUserIsOwner { get; set; }
}
