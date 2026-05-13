namespace House_renting_system_Project.Data.Data.Entities;

using System.ComponentModel.DataAnnotations;


using static Data.DataConstants.Category;

public class Category
{
    public int Id { get; init; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public IEnumerable<House> Houses { get; init; } = null!;
}
