namespace House_Renting_System_Project.Services.Models.Houses;

public class HouseQueryServiceModel
{
	public int? CategoryId { get; set; }

	public string? SearchText { get; set; }

	public string SortingType { get; set; } = "Asc";
}
