using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystemData.Data.Entities
{
    public class Agent
    {
        public const int PhoneNumberMaxLength = 15;

        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(PhoneNumberMaxLength)]
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public IEnumerable<House> ManagedHouses { get; set; } = new List<House>();
    }
}
