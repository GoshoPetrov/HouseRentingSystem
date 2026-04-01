using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystemData.Data.Entities
{
    public class Category
    {
        public const int NameMaxLength = 50;

        [Key]
        public int Id { get; init; }
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        public IEnumerable<House> Houses { get; init; } = new List<House>();
    }
}
