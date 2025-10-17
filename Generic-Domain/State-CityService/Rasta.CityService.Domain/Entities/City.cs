using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rasta.CityService.Domain.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long Id { get; set; }

        public required long ProvinceId { get; set; }

        public required string Name { get; set; }
        public Province Province { get; set; }
    }
}
