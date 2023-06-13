using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class UnitPlaceDTO:UnitPlace
    {
        public UnitPlaceDTO(UnitPlace up)
        {
            Id = up.Id;
            UnitId = up.UnitId;
            PlaceId = up.PlaceId;
            Comment = up.Comment;
            DateStart = up.DateStart;
            DateEnd = up.DateEnd;
            WorkOnRequestId = up.WorkOnRequestId;
        }

        [Key]
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PlaceId { get; set; }
        [StringLength(512)]
        public string? Comment { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }
        public int? WorkOnRequestId { get; set; }
    }
}
