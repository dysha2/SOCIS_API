using SOCIS_API.Model;

namespace SOCIS_API.Repositoryies
{
    public class ShortTermMoveRep : IShortTermMoveRep
    {
        private readonly EquipmentContext _context;

        public ShortTermMoveRep(EquipmentContext context)
        {
            _context = context;
        }

        public ShortTermMove Add(ShortTermMove STM)
        {
            var newSTM = new ShortTermMove
            {
                UnitId = STM.UnitId,
                PlaceId = STM.PlaceId,
                DateTimeStart = STM.DateTimeStart,
                WorkOnRequestId = STM.WorkOnRequestId,
                DateTimeEndPlan = STM.DateTimeEndPlan
            };
            _context.ShortTermMoves.Add(newSTM);
            _context.SaveChanges();
            return newSTM;
        }

        public ShortTermMove? Get(int id)
        {
            return LoadData(_context.ShortTermMoves.Where(x => x.ShortTermMoveId == id)).FirstOrDefault();
        }

        public List<ShortTermMove> GetAllActiveByPlace(int placeId)
        {
            return LoadData(_context.ShortTermMoves.Where(x => x.PlaceId == placeId && x.DateTimeEndFact == null)).ToList();
        }

        public List<ShortTermMove> GetAllByUnit(int unitId)
        {
            return LoadData(_context.ShortTermMoves.Where(x => x.UnitId==unitId)).ToList();
        }
        public List<ShortTermMove> GetAllActive()
        {
            return LoadData(_context.ShortTermMoves.Where(x => x.DateTimeEndFact == null)).ToList();
        }
        public List<ShortTermMove> GetAllOldByPlace(int placeId)
        {
            return LoadData(_context.ShortTermMoves.Where(x => x.PlaceId == placeId && x.DateTimeEndFact != null)).ToList();
        }
        private IQueryable<ShortTermMove> LoadData(IQueryable<ShortTermMove> items)
        {
            return items.Select(x => new ShortTermMoveDTO(x)
            {
                Unit = new AccountingUnitDTO(x.Unit)
                {
                    FullNameUnit = new FullNameUnitDTO(x.Unit.FullNameUnit)
                    {
                        Firm = x.Unit.FullNameUnit.Firm == null ? null : new FirmDTO(x.Unit.FullNameUnit.Firm),
                        UnitType = new UnitTypeDTO(x.Unit.FullNameUnit.UnitType)
                    }
                },
                Place = new PlaceDTO(x.Place),
                WorkOnRequest = x.WorkOnRequest == null ? null : new WorkOnRequestDTO(x.WorkOnRequest)
            });
        }
    }
}
