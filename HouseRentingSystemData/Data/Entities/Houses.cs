using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystemData.Data.Entities
{
    public class House
    {
        public const int TitleMaxLength = 50;
        public const int AddressMaxLength = 150;
        public const int DescriptionMaxLength = 500;


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

        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }
        public string? RenterId { get; set; }
        public IdentityUser? Renter { get; set; }

    }
}
