using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class ServiceDTO:Service
    {
        public ServiceDTO(Service ser)
        {
            Id = ser.Id;
            Name = ser.Name;
            Code = ser.Code;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(20)]
        public string Code { get; set; } = null!;
    }
}
