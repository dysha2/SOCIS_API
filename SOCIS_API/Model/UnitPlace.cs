using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("UnitPlace")]
    public partial class UnitPlace
    {
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

        [ForeignKey("PlaceId")]
        [InverseProperty("UnitPlaces")]
        public virtual Place Place { get; set; } = null!;
        [ForeignKey("UnitId")]
        [InverseProperty("UnitPlaces")]
        public virtual AccountingUnit Unit { get; set; } = null!;
        [ForeignKey("WorkOnRequestId")]
        [InverseProperty("UnitPlaces")]
        public virtual WorkOnRequest? WorkOnRequest { get; set; }
    }
}
