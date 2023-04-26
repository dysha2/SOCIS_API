using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Model
{
    [Table("AccountingUnit")]
    public partial class AccountingUnit
    {
        public AccountingUnit()
        {
            RequestUnits = new HashSet<RequestUnit>();
            ShortTermMoves = new HashSet<ShortTermMove>();
            UnitPlaces = new HashSet<UnitPlace>();
            UnitRespPeople = new HashSet<UnitRespPerson>();
        }

        [Key]
        public int Id { get; set; }
        [Column("MAC")]
        [StringLength(17)]
        public string? Mac { get; set; }
        [StringLength(50)]
        public string? SerNum { get; set; }
        [StringLength(15)]
        public string? NetName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ManufDate { get; set; }
        [StringLength(15)]
        public string? InvNum { get; set; }
        public int FullNameUnitId { get; set; }
        [StringLength(512)]
        public string? Comment { get; set; }

        [ForeignKey("FullNameUnitId")]
        [InverseProperty("AccountingUnits")]
        public virtual FullNameUnit FullNameUnit { get; set; } = null!;
        [InverseProperty("Unit")]
        public virtual ICollection<RequestUnit> RequestUnits { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<ShortTermMove> ShortTermMoves { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<UnitPlace> UnitPlaces { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<UnitRespPerson> UnitRespPeople { get; set; }
    }
}
