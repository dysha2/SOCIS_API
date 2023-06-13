namespace SOCIS_API.Repositoryies
{
    public class UnitPlaceRep : IUnitPlaceRep
    {
        private readonly EquipmentContext _context;

        public UnitPlaceRep(EquipmentContext context)
        {
            _context = context;
        }
        #region Get
        public UnitPlace? Get(int id)
        {
            return LoadData(_context.UnitPlaces.Where(x => x.Id == id)).FirstOrDefault();
        }

        public List<UnitPlace> GetAllActiveByPlace(int placeId)
        {
            return LoadData(_context.UnitPlaces.Where(x => x.PlaceId == placeId && x.DateEnd == null)).ToList();
        }
        public List<UnitPlace> GetAllOldByPlace(int placeId)
        {
            return LoadData(_context.UnitPlaces.Where(x => x.PlaceId == placeId && x.DateEnd != null)).ToList();
        }

        public List<UnitPlace> GetAllByUnit(int unitId)
        {
            return LoadData(_context.UnitPlaces.Where(x => x.UnitId == unitId)).ToList();
        }

        private IQueryable<UnitPlace> LoadData(IQueryable<UnitPlace> items)
        {
            return items.Select(x => new UnitPlaceDTO(x)
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

        #endregion

        #region Add
        public UnitPlace Add(UnitPlace unitPlace)
        {
            UnitPlace newUP = new UnitPlace
            {
                UnitId = unitPlace.UnitId,
                PlaceId = unitPlace.PlaceId,
                Comment = unitPlace.Comment,
                DateStart = unitPlace.DateStart,
                WorkOnRequestId = unitPlace.WorkOnRequestId
            };
            _context.UnitPlaces.Add(newUP);
            _context.SaveChanges();
            return newUP;
        }
        #endregion

    }
}
