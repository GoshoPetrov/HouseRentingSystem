namespace House_Renting_System_Project.Services.Contracts;

using House_Renting_System_Project.Services.Models.Houses;

public interface IHouseService
{
	Task<IReadOnlyCollection<HouseSummaryServiceModel>> GetAllAsync(
		HouseQueryServiceModel query,
		string? currentUserId);

	Task<HouseDetailsServiceModel?> GetDetailsAsync(int id);

	Task<IReadOnlyCollection<HouseCategoryServiceModel>> GetCategoriesAsync();

	Task<bool> CreateAsync(
		HouseFormServiceModel model,
		string userId);

	Task<IReadOnlyCollection<HouseSummaryServiceModel>> GetUserHousesAsync(
		string userId);

	Task<HouseFormServiceModel?> GetEditModelAsync(
		int id,
		string userId);

	Task<bool> UpdateAsync(
		HouseFormServiceModel model,
		string userId);

	Task<bool> DeleteAsync(
		int id,
		string userId);
}
