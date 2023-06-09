using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIS_API.ModelDTO
{
    public class AccountingUnitDTO:AccountingUnit
    {
        public AccountingUnitDTO(AccountingUnit AcUn)
        {
            Id = AcUn.Id;
            Mac = AcUn.Mac;
            SerNum = AcUn.SerNum;
            NetName = AcUn.NetName;
            ManufDate = AcUn.ManufDate;
            InvNum = AcUn.InvNum;
            FullNameUnitId = AcUn.FullNameUnitId;
            Comment = AcUn.Comment;
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
    }
}
