using System.Security.Claims;
using AspNetCoreGeneratedDocument;
using HouseRentingSystem.Models;
using HouseRentingSystem.Models.House;
using HouseRentingSystemData.Data;
using HouseRentingSystemData.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Controllers
{
    public class HouseController : Controller
    {

        private readonly HouseRentingDbContext _context;

        public HouseController(HouseRentingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AllHouses()
        {
            var currentUsersId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var housesViewModel = await _context.Houses
            .AsNoTracking()
            .Where(h => h.IsDeleted == false)
            .Select(h => new HouseViewModel
            {
                Id = h.Id,
                Name = h.Title,
                Address = h.Address,
                ImageUrl = h.ImageUrl,
                CurentUserIsOwner = h.AgentId == currentUsersId
            })
            .ToListAsync();
            ViewBag.Title = "All houses";
            return View(housesViewModel);
        }

        // GET: /House/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var searched = await _context.Houses
                .Include(h => h.Agent)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id);

            var model = new HouseDetailViewModel()
            {
                Id = searched.Id,
                Address = searched.Address,
                ImageUrl = searched.ImageUrl,
                Description = searched.Description,
                CreatedBy = searched.Agent.UserName,
                Price = searched.PricePerMonth,
                Name = searched.Title
            };

            return View(model);
        }

        // GET: /House/CreateHouse
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateHouse()
        {
            List<CreateHouseModel> ListOfCategories = await _context.Categories
           .AsNoTracking()
           .Select(c => new CreateHouseModel
           {
               Id = c.Id,
               Name = c.Name,
           })
           .ToListAsync();
            var houseCategories = new HouseFromViewModel()
            {
                Categories = ListOfCategories
            };

            return View(houseCategories);
        }

        // POST: /House/CreateHouse
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHouse(HouseFromViewModel model)
        {
            var houseCategories = await GetCategories();

            if (!ModelState.IsValid)
            {

                model.Categories = houseCategories;
                return View(model);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool addressExists = await _context.Houses
                .AnyAsync(h => h.Address.ToLower() == model.Address.ToLower());

            if (addressExists)
            {
                model.Categories = houseCategories;
                ModelState.AddModelError("Address", "This address is already registered");
                return View(model);
            }

            var newHouse = new House
            {
                Title = model.Title,
                Address = model.Address,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.SelectedCategoryId,
                AgentId = userId
            };

            _context.Houses.Add(newHouse);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AllHouses));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyHouses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var houses = _context.Houses
                .Where(h => h.AgentId == userId && h.IsDeleted == false)
                .Select(h => new HouseViewModel
                {
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    Name = h.Title,
                    Id = h.Id,
                    CurentUserIsOwner = true
                })
                .ToListAsync();
            ViewBag.Title = "My houses";
            return View(nameof(AllHouses), houses);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            var houseCategories = await GetCategories();

            var model = new HouseFromViewModel()
            {
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                Description = house.Description,
                Title = house.Title,
                PricePerMonth = house.PricePerMonth,
                Categories = houseCategories,
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(HouseFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var houseCategories = await GetCategories();
                return View(model);
            }
            var house = await _context.Houses.FindAsync(model.Id);
            house.PricePerMonth = model.PricePerMonth;
            house.Address = model.Address;
            house.ImageUrl = model.ImageUrl;
            house.Description = model.Description;
            house.Title = model.Title;
            house.CategoryId = model.SelectedCategoryId;

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(MyHouses));
        }

        private async Task<List<CreateHouseModel>> GetCategories()
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new CreateHouseModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        private async Task<IEnumerable<Ca>>
    }
}
