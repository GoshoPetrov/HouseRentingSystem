using House_renting_system_Project.Models.Query;

namespace House_renting_system_Project.Models.House
{
	public class AllHousesViewModel
	{
		public QueryViewModel Query { get; set; } = new QueryViewModel();
		public List<HousesViewModel> Houses { get; set; } = new List<HousesViewModel>();
		public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}
