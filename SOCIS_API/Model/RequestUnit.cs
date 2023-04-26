using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    public partial class RequestUnit
    {
        [Key]
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int WorkOnRequestId { get; set; }

        [ForeignKey("UnitId")]
        [InverseProperty("RequestUnits")]
        public virtual AccountingUnit Unit { get; set; } = null!;
        [ForeignKey("WorkOnRequestId")]
        [InverseProperty("RequestUnits")]
        public virtual WorkOnRequest WorkOnRequest { get; set; } = null!;
    }
}
