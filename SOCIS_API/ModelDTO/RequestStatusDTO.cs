using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class RequestStatusDTO:RequestStatus
    {
        public RequestStatusDTO(RequestStatus ReqStat)
        {
            Id = ReqStat.Id;
            Name = ReqStat.Name;
            Code = ReqStat.Code;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(20)]
        public string Code { get; set; } = null!;
    }
}
