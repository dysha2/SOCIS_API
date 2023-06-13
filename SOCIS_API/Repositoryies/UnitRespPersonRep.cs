using SOCIS_API.Model;

namespace SOCIS_API.Repositoryies
{
    public class UnitRespPersonRep : IUnitRespPersonRep
    {
        private readonly EquipmentContext _context;

        public UnitRespPersonRep(EquipmentContext context)
        {
            _context = context;
        }
        #region Get
        public UnitRespPerson? Get(int id)
        {
            return LoadData(_context.UnitRespPeople.Where(x => x.Id == id)).FirstOrDefault();
        }

        public List<UnitRespPerson> GetAllActiveByPerson(int personId)
        {
            return LoadData(_context.UnitRespPeople.Where(x => x.PersonId == personId && x.DateEnd == null)).ToList();
        }

        public List<UnitRespPerson> GetAllByUnit(int unitId)
        {
            return LoadData(_context.UnitRespPeople.Where(x => x.UnitId == unitId)).ToList();
        }

        public List<UnitRespPerson> GetAllOldByPerson(int personId)
        {
            return LoadData(_context.UnitRespPeople.Where(x => x.PersonId == personId && x.DateEnd != null)).ToList();
        }

        private IQueryable<UnitRespPerson> LoadData(IQueryable<UnitRespPerson> items)
        {
            return items.Select(x => new UnitRespPersonDTO(x)
            {
                Unit = new AccountingUnitDTO(x.Unit)
                {
                    FullNameUnit = new FullNameUnitDTO(x.Unit.FullNameUnit)
                    {
                        Firm = x.Unit.FullNameUnit.Firm == null ? null : new FirmDTO(x.Unit.FullNameUnit.Firm),
                        UnitType = new UnitTypeDTO(x.Unit.FullNameUnit.UnitType)
                    }
                },
                Person = new PersonDTO(x.Person)
            });
        }
        #endregion

        #region Add
        public UnitRespPerson Add(UnitRespPerson upr)
        {
            UnitRespPerson newUpr = new UnitRespPerson
            {
                UnitId = upr.UnitId,
                PersonId=upr.PersonId,
                DateStart = upr.DateStart
            };
            _context.UnitRespPeople.Add(newUpr);
            _context.SaveChanges();
            return newUpr;
        }
        #endregion

        #region Update
        public void Update(int uprId, UnitRespPerson upr)
        {
            var oldUpr = _context.UnitRespPeople.Find(uprId);
            if (oldUpr is null) throw new Exception("Not found");
            oldUpr.DateStart = upr.DateStart;
            oldUpr.UnitId = upr.UnitId;
            oldUpr.DateEnd = upr.DateEnd;
            oldUpr.PersonId = upr.PersonId;
            _context.SaveChanges();
        }
        #endregion
    }
}
