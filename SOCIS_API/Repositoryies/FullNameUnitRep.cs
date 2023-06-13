namespace SOCIS_API.Repositoryies
{
    public class FullNameUnitRep : IFullNameUnitRep
    {
        private readonly EquipmentContext _context;

        public FullNameUnitRep(EquipmentContext context)
        {
            _context = context;
        }

        public FullNameUnit? Get(int id)
        {
            return LoadData(_context.FullNameUnits.Where(x => x.Id == id)).FirstOrDefault();
        }

        public List<FullNameUnit> GetAll()
        {
            return LoadData(_context.FullNameUnits).ToList();
        }
        private IQueryable<FullNameUnit> LoadData(IQueryable<FullNameUnit> items)
        {
            return items
                .Select(x => new FullNameUnitDTO(x)
                {
                    Firm = x.Firm == null?null: new FirmDTO(x.Firm),
                    UnitType = new UnitTypeDTO(x.UnitType)
                });
        }
    }
}
