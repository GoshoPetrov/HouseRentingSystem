using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : Controller
    {
        private List<HouseViewModel> houses = new List<HouseViewModel>()
        {
            new HouseViewModel()
            {
                Id= 1,
                Name = "Beach house",
                Address = "Miami, Florida",
                ImageUrl = @""

            },
            new HouseViewModel()
            {
                Id = 2,
                Name = "Mountin house",
                Address = "Rila, Bulgaria",
                ImageUrl = @""
            },
            new HouseViewModel()
            {
                Id = 3,
                Name = "Urban house",
                Address = "Lulin, Sofia",
                ImageUrl = @""
            }
        };
        public IActionResult AllHouses()
        {
            return View(houses);
        }

        public IActionResult Details(int Id)
        {
            var d = houses.FirstOrDefault(h => h.Id == Id);
            return View(d);
        }
    }
}
