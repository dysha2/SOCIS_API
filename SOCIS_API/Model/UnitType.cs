using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("UnitType")]
    public partial class UnitType
    {
        public UnitType()
        {
            FullNameUnits = new HashSet<FullNameUnit>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;

        [InverseProperty("UnitType")]
        public virtual ICollection<FullNameUnit> FullNameUnits { get; set; }
    }
}
