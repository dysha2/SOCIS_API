using SOCIS_API.Model;
using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class PlaceDTO:Place
    {
        public PlaceDTO(Place place)
        {
            Id = place.Id;
            Name = place.Name;
            Description = place.Description;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(5)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string? Description { get; set; }
    }
}
