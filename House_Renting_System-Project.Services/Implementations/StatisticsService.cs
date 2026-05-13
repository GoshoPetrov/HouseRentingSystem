namespace House_Renting_System_Project.Services.Implementations;

using Contracts;

public class StatisticsService : IStatisticsService
{
	public int TotalRequests { get; private set; }

	public int HouseSearches { get; private set; }

	public void RegisterRequest()
		=> this.TotalRequests++;

	public void RegisterHouseSearch()
		=> this.HouseSearches++;
}
