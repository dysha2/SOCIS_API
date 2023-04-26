using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("UnitRespPerson")]
    public partial class UnitRespPerson
    {
        [Key]
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int PersonId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("UnitRespPeople")]
        public virtual Person Person { get; set; } = null!;
        [ForeignKey("UnitId")]
        [InverseProperty("UnitRespPeople")]
        public virtual AccountingUnit Unit { get; set; } = null!;
    }
}
