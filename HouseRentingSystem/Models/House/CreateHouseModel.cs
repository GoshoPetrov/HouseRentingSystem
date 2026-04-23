using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Models.House
{
    public class CreateHouseModel
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
    }
}
