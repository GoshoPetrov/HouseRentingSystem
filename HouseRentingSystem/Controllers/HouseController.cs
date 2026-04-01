using HouseRentingSystem.Models;
using HouseRentingSystemData.Data;
using HouseRentingSystemData.Data.Entities;
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

        // GET: /House/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var house = await _context.Houses
                .Where(h => h.Id == id)
                .Select(h => new HouseViewModel
                {
                    Id = h.Id,
                    Name = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl
                })
                .FirstOrDefaultAsync();

            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // GET: /House/CreateHouse
        [HttpGet]
        public IActionResult CreateHouse()
        {
            return View();
        }

        // POST: /House/CreateHouse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHouse(HouseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Map ViewModel to your House entity
            var house = new House
            {
                Title = model.Name,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                Description = "",
                CategoryId = 1,
                PricePerMonth = 42.4m,

            };

            _context.Houses.Add(house);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AllHouses));
        }
    }
}
