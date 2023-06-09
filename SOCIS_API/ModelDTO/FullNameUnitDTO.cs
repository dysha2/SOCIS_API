using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class FullNameUnitDTO:FullNameUnit
    {
        public FullNameUnitDTO(FullNameUnit fnu)
        {
            Id = fnu.Id;
            FirmId = fnu.FirmId;
            Model = fnu.Model;
            UnitTypeId = fnu.UnitTypeId;
            ModelNo = fnu.ModelNo;
        }

        [Key]
        public int Id { get; set; }
        public int? FirmId { get; set; }
        [StringLength(50)]
        public string Model { get; set; } = null!;
        public int UnitTypeId { get; set; }
        [StringLength(30)]
        public string? ModelNo { get; set; }
    }
}
