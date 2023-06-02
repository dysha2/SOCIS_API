using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class WorkOnRequestDTO:WorkOnRequest
    {
        public WorkOnRequestDTO(WorkOnRequest wor)
        {
            Id = wor.Id;
            RequestId = wor.RequestId;
            ServiceId = wor.ServiceId;
            DateTime = wor.DateTime;
            Comment = wor.Comment;
            ImplementerId = wor.ImplementerId;
        }

        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ServiceId { get; set; }
        [Precision(0)]
        public DateTime DateTime { get; set; }
        [StringLength(1000)]
        public string? Comment { get; set; }
        public int ImplementerId { get; set; }
    }
}
