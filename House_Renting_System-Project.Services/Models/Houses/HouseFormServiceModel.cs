namespace House_Renting_System_Project.Services.Models.Houses;

public class HouseFormServiceModel
{
	public int Id { get; set; }

	public string Title { get; set; } = "";

	public string Description { get; set; } = "";

	public string Address { get; set; } = "";

	public decimal PricePerMonth { get; set; }

	public string ImageUrl { get; set; } = "";

	public int SelectedCategoryId { get; set; }
}
