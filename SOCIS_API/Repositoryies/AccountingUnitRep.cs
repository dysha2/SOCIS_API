namespace SOCIS_API.Repositoryies
{
    public class AccountingUnitRep : IAccountingUnitRep
    {
        private readonly EquipmentContext _context;

        public AccountingUnitRep(EquipmentContext context)
        {
            _context = context;
        }

        public AccountingUnit? Get(int id)
        {
            return LoadData(_context.AccountingUnits.Where(x => x.Id == id)).FirstOrDefault();
        }

        public List<AccountingUnit> GetAll()
        {
            return LoadData(_context.AccountingUnits).ToList();
        }

        public List<AccountingUnit> GetAllByFullNameUnit(int fnuId)
        {
            return LoadData(_context.AccountingUnits.Where(x=>x.FullNameUnitId==fnuId)).ToList();
        }
        //public List<AccountingUnit> GetAllByPlace(int placeId)
        //{
        //    return LoadData(_context.AccountingUnits.Where(x => x.CurrentPlace.Id == placeId)).ToList();
        //}
        //public List<AccountingUnit> GetAllByPerson(int personId)
        //{
        //    throw new NotImplementedException();
        //}
        private IQueryable<AccountingUnit> LoadData(IQueryable<AccountingUnit> items)
        {
            return items.Select(x => new AccountingUnitDTO(x)
            {
                FullNameUnit = new FullNameUnitDTO(x.FullNameUnit)
                {
                    Firm = x.FullNameUnit.Firm == null ? null : new FirmDTO(x.FullNameUnit.Firm),
                    UnitType = new UnitTypeDTO(x.FullNameUnit.UnitType)
                }
            });
        }
    }
}
