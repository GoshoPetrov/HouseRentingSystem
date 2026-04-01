using HouseRentingSystemData.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HouseRentingSystemData.Data;

public class HouseRentingDbContext : IdentityDbContext<HouseRentingSystemData.Data.Entities.ApplicationUser>
{
    // Seed data holders
    private ApplicationUser agentUser = null!;
    private ApplicationUser guestUser = null!;
    private Agent agent = null!;
    private Category cottageCategory = null!;
    private Category singleCategory = null!;
    private Category duplexCategory = null!;
    private House firstHouse = null!;
    private House secondHouse = null!;
    private House thirdHouse = null!;

    public HouseRentingDbContext(DbContextOptions<HouseRentingDbContext> options)
        : base(options)
    {
    }

    public DbSet<House> Houses { get; init; } = null!;
    public DbSet<Category> Categories { get; init; } = null!;
    public DbSet<Agent> Agents { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Agent>()
            .Ignore(a => a.User);

        builder.Entity<House>()
            .Ignore(a => a.Renter);
        
        builder.Entity<House>()
            .HasOne(h => h.Category)
            .WithMany(c => c.Houses)
            .HasForeignKey(h => h.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<House>()
            .HasOne(h => h.Agent)
            .WithMany(a => a.ManagedHouses)
            .HasForeignKey(h => h.AgentId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedUsers();
        SeedAgent();
        SeedCategories();
        SeedHouses();

        builder.Entity<Agent>().HasData(agent);
        builder.Entity<Category>().HasData(cottageCategory, singleCategory, duplexCategory);
        builder.Entity<House>().HasData(firstHouse, secondHouse, thirdHouse);
    }

    private void SeedUsers()
    {
        var hasher = new PasswordHasher<HouseRentingSystemData.Data.Entities.ApplicationUser>();

        agentUser = new ApplicationUser
        {
            Id = "dea12856-c198-4129-b3f3-b893d8395082",
            UserName = "agent@mail.com",
            NormalizedUserName = "AGENT@MAIL.COM",
            Email = "agent@mail.com",
            NormalizedEmail = "AGENT@MAIL.COM",
            EmailConfirmed = true
        };

        agentUser.PasswordHash =
            hasher.HashPassword(agentUser, "agent123");

        guestUser = new ApplicationUser
        {
            Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
            UserName = "guest@mail.com",
            NormalizedUserName = "GUEST@MAIL.COM",
            Email = "guest@mail.com",
            NormalizedEmail = "GUEST@MAIL.COM",
            EmailConfirmed = true
        };

        guestUser.PasswordHash =
            hasher.HashPassword(guestUser, "guest123");
    }

    private void SeedAgent()
    {
        agent = new Agent
        {
            Id = Guid.Parse("44a41a1c-943b-47e2-80e6-47463b6f139b"),
            PhoneNumber = "+359888888888",
            UserId = agentUser.Id
        };
    }

    private void SeedCategories()
    {
        cottageCategory = new Category
        {
            Id = 1,
            Name = "Cottage"
        };

        singleCategory = new Category
        {
            Id = 2,
            Name = "Single-Family"
        };

        duplexCategory = new Category
        {
            Id = 3,
            Name = "Duplex"
        };
    }

    private void SeedHouses()
    {
        firstHouse = new House
        {
            Id = 1,
            Title = "Big House Marina",
            Address = "North London, UK (near the border)",
            Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
            ImageUrl = "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
            PricePerMonth = 2100.00M,
            CategoryId = duplexCategory.Id,
            AgentId = agent.Id,
            RenterId = guestUser.Id
        };

        secondHouse = new House
        {
            Id = 2,
            Title = "Family House Comfort",
            Address = "Near the Sea Garden in Burgas, Bulgaria",
            Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
            ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg",
            PricePerMonth = 1200.00M,
            CategoryId = singleCategory.Id,
            AgentId = agent.Id
        };

        thirdHouse = new House
        {
            Id = 3,
            Title = "Grand House",
            Address = "Boyana Neighbourhood, Sofia, Bulgaria",
            Description = "This luxurious house is everything you will need. It is just excellent.",
            ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
            PricePerMonth = 2000.00M,
            CategoryId = singleCategory.Id,
            AgentId = agent.Id
        };
    }
}