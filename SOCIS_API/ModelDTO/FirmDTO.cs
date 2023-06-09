using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class FirmDTO:Firm
    {
        public FirmDTO(Firm firm)
        {
            Id = firm.Id;
            Name = firm.Name;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;
    }
}
