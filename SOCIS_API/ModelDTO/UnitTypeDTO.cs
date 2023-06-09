using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class UnitTypeDTO:UnitType
    {
        public UnitTypeDTO(UnitType unitType)
        {
            Id = unitType.Id;
            Name = unitType.Name;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;
    }
}
