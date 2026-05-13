using System.ComponentModel.DataAnnotations;
using static House_renting_system_Project.Data.Data.DataConstants.House;

namespace House_renting_system_Project.Models.House
{
	public class CreateHouseModel
	{

		public int Id { get; set; }
		[Required(ErrorMessage = "Моля напиши нещо!!!")]
		[StringLength(TitleMaxLength,MinimumLength = 10)]
		public string Title { get; set; } = string.Empty;

		[Required]
		[StringLength(DescriptionMaxLength,MinimumLength = 50)]
		public string Description { get; set; } = string.Empty;

		[Required]
		[StringLength(150, MinimumLength =20)]
		public string Address { get; set; } = string.Empty;

		[Required]
		[Range(0,double.MaxValue)]
		public decimal PricePerMonth { get; set; }

		[Required]
		public string ImageUrl { get; set; } = string.Empty;
		public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

		[Required]
		public int SelectedCategoryId { get; set; }

	}
}
