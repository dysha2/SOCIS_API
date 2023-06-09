using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class RequestUnitsDTO:RequestUnit
    {
        public RequestUnitsDTO(RequestUnit ReqUn)
        {
            Id = ReqUn.Id;
            UnitId = ReqUn.UnitId;
            WorkOnRequestId = ReqUn.WorkOnRequestId;
        }

        [Key]
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int WorkOnRequestId { get; set; }
    }
}
