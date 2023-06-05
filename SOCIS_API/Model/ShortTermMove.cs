using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("ShortTermMove")]
    public partial class ShortTermMove
    {
        [Key]
        public int ShortTermMoveId { get; set; }
        public int PlaceId { get; set; }
        public int? WorkOnRequestId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime? DateTimeEndPlan { get; set; }
        public DateTime? DateTimeEndFact { get; set; }
        public int UnitId { get; set; }

        [ForeignKey("PlaceId")]
        [InverseProperty("ShortTermMoves")]
        public virtual Place Place { get; set; } = null!;
        [ForeignKey("UnitId")]
        [InverseProperty("ShortTermMoves")]
        public virtual AccountingUnit Unit { get; set; } = null!;
        [ForeignKey("WorkOnRequestId")]
        [InverseProperty("ShortTermMoves")]
        public virtual WorkOnRequest? WorkOnRequest { get; set; }
    }
}
