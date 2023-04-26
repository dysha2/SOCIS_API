using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Place")]
    [Index("Name", Name = "UQ__Place__737584F638D82339", IsUnique = true)]
    public partial class Place
    {
        public Place()
        {
            Requests = new HashSet<Request>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(5)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string? Description { get; set; }

        [InverseProperty("Place")]
        public virtual ICollection<Request> Requests { get; set; }
        [InverseProperty("Place")]
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        [InverseProperty("Place")]
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
    }
}
