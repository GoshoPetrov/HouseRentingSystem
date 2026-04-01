using HouseRentingSystem.Models;
using HouseRentingSystemData.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        private readonly HouseRentingDbContext _context;

        public HouseController(HouseRentingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AllHouses()
        {

            // Fetch all houses from the database and map to HouseViewModel
            var houses = await _context.Houses
                .Select(h => new HouseViewModel
                {
                    Id = h.Id,
                    Name = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl
                })
                .ToListAsync();

            return View(houses);
        }

        public IActionResult Details(int Id)
        {
            var d = houses.FirstOrDefault(h => h.Id == Id);
            return View(d);
        }
    }
}
