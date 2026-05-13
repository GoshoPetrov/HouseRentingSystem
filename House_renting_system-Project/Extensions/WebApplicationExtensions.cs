namespace House_renting_system_Project.Extensions;

using Data.Data.Entities;
using House_renting_system_Project.Data.Data;

public static class WebApplicationExtensions
{
    extension(WebApplication app)
    {
        public async Task SeedHouses()
        {
            using var scope = app.Services.CreateScope();

            var data = scope
                .ServiceProvider
                .GetRequiredService<HouseRentingDbContext>();

            if (!data.Houses.Any())
            {
                data.AddRange(
                    new House
                    {
                        Title = "Big House Marina",
                        Address = "North London, UK (near the border)",
                        Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                        ImageUrl = "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
                        PricePerMonth = 2100.00M,
                        CategoryId = 5,
                        AgentId = "1f85374b-dcc0-44a9-9042-de8dc94b661a"
                    },
                    new House
                    {
                        Title = "Family House Comfort",
                        Address = "Near the Sea Garden in Burgas, Bulgaria",
                        Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                        ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jp?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                        PricePerMonth = 1200.00M,
                        CategoryId = 5,
                        AgentId = "1f85374b-dcc0-44a9-9042-de8dc94b661a"
                    },
                    new House
                    {
                        Title = "Modern Loft",
                        Address = "33 Industrial Zone, Sofia",
                        Description = "Stylish loft with open space design and high ceilings.",
                        ImageUrl = "https://images.unsplash.com/photo-1484154218962-a197022b5858",
                        PricePerMonth = 1000.00M,
                        CategoryId = 4,
                        AgentId = "1f85374b-dcc0-44a9-9042-de8dc94b661a"
                    },
                    new House
                    {
                        Title = "Budget Room",
                        Address = "18 Studentski Grad, Sofia",
                        Description = "Affordable room suitable for students.",
                        ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85",
                        PricePerMonth = 400.00M,
                        CategoryId = 4,
                        AgentId = "1f85374b-dcc0-44a9-9042-de8dc94b661a"
                    });

                await data.SaveChangesAsync();
            }


        }
    }
}

public static class RoleNames
{
    public static readonly string[] Names = [Agent, Client];
    public const string Agent = "Agent";
    public const string Client = "Client";
}

public static class UserCredentials
{
    public const string DefaultAgentUsername = "agent-def";
    public const string DefaultAgentEmail = "agent@mail.com";
    public const string DefaultAgentPassword = "Qerty78_";

}
