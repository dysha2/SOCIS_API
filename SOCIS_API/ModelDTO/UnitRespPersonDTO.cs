using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class UnitRespPersonDTO:UnitRespPerson
    {
        public UnitRespPersonDTO(UnitRespPerson urp)
        {
            Id = urp.Id;
            UnitId = urp.UnitId;
            PersonId = urp.PersonId;
            DateStart = urp.DateStart;
            DateEnd = urp.DateEnd;
        }

        [Key]
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PersonId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }
    }
}
