namespace House_Renting_System_Project.Services.Implementations;

using House_renting_system_Project.Data.Data;
using House_renting_system_Project.Data.Data.Entities;
using House_Renting_System_Project.Services.Contracts;
using House_Renting_System_Project.Services.Models.Houses;
using Microsoft.EntityFrameworkCore;

public class HouseService(HouseRentingDbContext data) : IHouseService
{
    public async Task<IReadOnlyCollection<HouseSummaryServiceModel>> GetAllAsync(
		HouseQueryServiceModel query,
		string? currentUserId)
	{
		var housesQuery = data.Houses
			.AsNoTracking();

		if (!string.IsNullOrWhiteSpace(query.SearchText))
		{
			housesQuery = housesQuery
				.Where(h => h.Description.Contains(query.SearchText));
		}

		if (query.CategoryId.HasValue && query.CategoryId.Value != 0)
		{
			housesQuery = housesQuery
				.Where(h => h.CategoryId == query.CategoryId.Value);
		}

		housesQuery = query.SortingType == "Desc"
			? housesQuery.OrderByDescending(h => h.Title)
			: housesQuery.OrderBy(h => h.Title);

		return await housesQuery
			.Select(h => new HouseSummaryServiceModel
			{
				Address = h.Address,
				Id = h.Id,
				ImageUrl = h.ImageUrl,
				Name = h.Title,
				CurrentUserIsOwner = h.AgentId == currentUserId,
            })
			.ToListAsync();
	}

	public async Task<HouseDetailsServiceModel?> GetDetailsAsync(int id)
		=> await data.Houses
			.Where(h => h.Id == id)
			.Select(h => new HouseDetailsServiceModel
			{
				Id = h.Id,
				Address = h.Address,
				Description = h.Description,
				CreatedBy = h.Agent.UserName ?? "",
				ImageUrl = h.ImageUrl,
				Price = h.PricePerMonth,
				Name = h.Title
			})
			.FirstOrDefaultAsync();

	public async Task<IReadOnlyCollection<HouseCategoryServiceModel>> GetCategoriesAsync()
		=> await data.Categories
			.Select(c => new HouseCategoryServiceModel
			{
				Id = c.Id,
				Name = c.Name
			})
			.ToListAsync();

	public async Task<bool> CreateAsync(HouseFormServiceModel model, string userId)
	{
		var house = new House
		{
			Description = model.Description,
			Address = model.Address,
			CategoryId = model.SelectedCategoryId,
			PricePerMonth = model.PricePerMonth,
			ImageUrl = model.ImageUrl,
			Title = model.Title,
			AgentId = userId
		};

		data.Add(house);
		await data.SaveChangesAsync();

		return true;
	}

    public async Task<IReadOnlyCollection<HouseSummaryServiceModel>> GetUserHousesAsync(string userId)
		=> await data
			.Houses
            .Where(h => h.AgentId == userId)
            .Select(h => new HouseSummaryServiceModel
            {
                Address = h.Address,
                ImageUrl = h.ImageUrl,
                Name = h.Title,
                Id = h.Id,
                CurrentUserIsOwner = true
            })
            .ToListAsync();

    public async Task<HouseFormServiceModel?> GetEditModelAsync(
		int id,
		string userId)
	{
		var house = await data
			.Houses
			.AsNoTracking()
			.FirstOrDefaultAsync(h => h.Id == id);

		if (house is null || house.AgentId != userId)
		{
			return null;
		}

		return new()
		{
			Id = house.Id,
			Address = house.Address,
			Description = house.Description,
			ImageUrl = house.ImageUrl,
			PricePerMonth = house.PricePerMonth,
			Title = house.Title,
			SelectedCategoryId = house.CategoryId
		};
	}

	public async Task<bool> UpdateAsync(
		HouseFormServiceModel model,
		string userId)
	{
		var house = await data
			.Houses
			.FirstOrDefaultAsync(h => h.Id == model.Id);

		if (house is null || house.AgentId != userId)
		{
			return false;
		}

		house.Description = model.Description;
		house.ImageUrl = model.ImageUrl;
		house.Address = model.Address;
		house.PricePerMonth = model.PricePerMonth;
		house.Title = model.Title;
		house.CategoryId = model.SelectedCategoryId;

		await data.SaveChangesAsync();

		return true;
	}

	public async Task<bool> DeleteAsync(
		int id,
		string userId)
	{
		var house = await data
			.Houses
			.FirstOrDefaultAsync(h => h.Id == id);

		if (house is null || house.AgentId != userId)
		{
			return false;
		}

		house.IsDeleted = true;
		await data.SaveChangesAsync();

		return true;
	}

    public async Task<bool> RentAsync(
		int houseId,
		string userRenterId)
    {
		var house = await this.GetHouse(houseId);
		if (house is null || house.RenterId is not null)
		{
			return false;
		}

		house.RenterId = userRenterId;
		await data.SaveChangesAsync();

		return true;
    }

    public async Task<bool> LeaveAsync(
		int houseId,
		string userRenterId)
    {
        var house = await this.GetHouse(houseId);
        if (house is null || house.RenterId is null)
        {
            return false;
        }

        house.RenterId = null;
        await data.SaveChangesAsync();

        return true;
    }

	private async Task<House?> GetHouse(int id)
		=> await data
			.Houses
			.FirstOrDefaultAsync(h => h.Id == id);
}
