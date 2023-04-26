using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("Firm")]
    [Index("Name", Name = "UQ__Firm__737584F6CC91B4DD", IsUnique = true)]
    public partial class Firm
    {
        public Firm()
        {
            FullNameUnits = new HashSet<FullNameUnit>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;

        [InverseProperty("Firm")]
        public virtual ICollection<FullNameUnit> FullNameUnits { get; set; }
    }
}
