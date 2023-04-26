using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("WorkOnRequest")]
    public partial class WorkOnRequest
    {
        public WorkOnRequest()
        {
            RequestUnits = new HashSet<RequestUnit>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
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

        [ForeignKey("ImplementerId")]
        [InverseProperty("WorkOnRequests")]
        public virtual Person Implementer { get; set; } = null!;
        [ForeignKey("RequestId")]
        [InverseProperty("WorkOnRequests")]
        public virtual Request Request { get; set; } = null!;
        [ForeignKey("ServiceId")]
        [InverseProperty("WorkOnRequests")]
        public virtual Service Service { get; set; } = null!;
        [InverseProperty("WorkOnRequest")]
        public virtual ICollection<RequestUnit> RequestUnits { get; set; }
        [InverseProperty("WorkOnRequest")]
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        [InverseProperty("WorkOnRequest")]
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
    }
}
