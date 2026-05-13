using House_renting_system_Project.Models.House;

namespace House_renting_system_Project.Models.Query
{
	public class QueryViewModel
	{
		public int? CategoryId {  get; set; }

		public string? SearchText { get; set; }

		public string SortingType { get; set; } = "Asc";
	}
}
