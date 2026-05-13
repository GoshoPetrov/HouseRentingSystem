namespace House_renting_system_Project.Models.House;

public class HousesViewModel
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Address { get; set; } = string.Empty;

	public string ImageUrl { get; set; } = string.Empty;

	public bool CurrentUserIsOwner { get; set; }
}
