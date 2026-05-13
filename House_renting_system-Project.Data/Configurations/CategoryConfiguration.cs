namespace House_renting_system_Project.Data.Configurations;

using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
        => builder.HasData(SeedCategories());

    private static Category[] SeedCategories()
        => [
            new()
            {
                Id = 1,
                Name = "Cottage"
            },
            new()
            {
                Id = 2,
                Name = "Single-Family House"
            },
            new()
            {
                Id = 3,
                Name = "Duplex"
            },
            new()
            {
                Id = 4,
                Name = "One Bedroom"
            },
            new()
            {
                Id = 5,
                Name = "Double Bedroom"
            }
        ];
}
