using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.ModelDTO
{
    public class ShortTermMoveDTO:ShortTermMove
    {
        public ShortTermMoveDTO(ShortTermMove stm)
        {
            ShortTermMoveId = stm.ShortTermMoveId;
            PlaceId = stm.PlaceId;
            WorkOnRequestId = stm.WorkOnRequestId;
            DateTimeStart = stm.DateTimeStart;
            DateTimeEndPlan = stm.DateTimeEndPlan;
            DateTimeEndFact = stm.DateTimeEndFact;
            UnitId = stm.UnitId;
        }

        [Key]
        public int ShortTermMoveId { get; set; }
        public int PlaceId { get; set; }
        public int? WorkOnRequestId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime? DateTimeEndPlan { get; set; }
        public DateTime? DateTimeEndFact { get; set; }
        public int UnitId { get; set; }
    }
}
