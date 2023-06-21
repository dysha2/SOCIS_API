using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class RequestDTO:Request
    {
        public RequestDTO(Request req)
        {
            Id = req.Id;
            DeclarantId = req.DeclarantId;
            DateTimeStart = req.DateTimeStart;
            Description = req.Description;
            DateTimeEnd = req.DateTimeEnd;
            PlaceId = req.PlaceId;
            IsComplete = req.IsComplete;
            RequestStatusId = req.RequestStatusId;
            Importance = req.Importance;
        }

        [Key]
        public int Id { get; set; }
        public int DeclarantId { get; set; }
        [Precision(0)]
        public DateTime DateTimeStart { get; set; }
        [StringLength(1000)]
        public string Description { get; set; } = null!;
        [Precision(0)]
        public DateTime? DateTimeEnd { get; set; }
        public int PlaceId { get; set; }
        public bool IsComplete { get; set; }
        public int RequestStatusId { get; set; }
        public int? Importance { get; set; }
    }

}
