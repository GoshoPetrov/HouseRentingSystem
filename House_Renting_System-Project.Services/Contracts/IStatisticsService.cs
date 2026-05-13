namespace House_Renting_System_Project.Services.Contracts;

public interface IStatisticsService
{
	int TotalRequests { get; }

	int HouseSearches { get; }

	void RegisterRequest();

	void RegisterHouseSearch();
}
