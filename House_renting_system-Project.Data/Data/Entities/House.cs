namespace House_renting_system_Project.Data.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Data.DataConstants.House;

public class House
{
    [Key]
    public int Id { get; init; }

    [MaxLength(TitleMaxLength)]
    [MinLength(10)]
    [Required]
    public string Title { get; set; } = null!;

    [MaxLength(AddressMaxLength)]
    [MinLength(30)]
    [Required]
    public string Address { get; set; } = null!;

    [MaxLength(DescriptionMaxLength)]
    [MinLength(50)]
    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [MaxLength(2000)]
    [Required]
    [Column(TypeName = "decimal(12, 3)")]
    public decimal PricePerMonth { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; init; } = null!;

    [ForeignKey(nameof(Agent))]
    public string AgentId { get; set; } = null!;

    public ApplicationUser Agent { get; set; } = null!;

    public string? RenterId { get; set; }

    public ApplicationUser? Renter { get; set; }

	public bool IsDeleted { get; set; }
}