using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("FullNameUnit")]
    [Index("FirmId", "Model", "UnitTypeId", "ModelNo", Name = "NoRepeat", IsUnique = true)]
    public partial class FullNameUnit
    {
        public FullNameUnit()
        {
            AccountingUnits = new HashSet<AccountingUnit>();
        }

        [Key]
        public int Id { get; set; }
        public int? FirmId { get; set; }
        [StringLength(50)]
        public string Model { get; set; } = null!;
        public int UnitTypeId { get; set; }
        [StringLength(30)]
        public string? ModelNo { get; set; }

        [ForeignKey("FirmId")]
        [InverseProperty("FullNameUnits")]
        public virtual Firm? Firm { get; set; }
        [ForeignKey("UnitTypeId")]
        [InverseProperty("FullNameUnits")]
        public virtual UnitType UnitType { get; set; } = null!;
        [InverseProperty("FullNameUnit")]
        public virtual ICollection<AccountingUnit> AccountingUnits { get; set; }
    }
}
