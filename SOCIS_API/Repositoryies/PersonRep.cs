namespace SOCIS_API.Repositoryies
{
    public class PersonRep : IPersonRep
    {
        EquipmentContext _context;

        public PersonRep(EquipmentContext context)
        {
            _context = context;
        }

        #region Get
        public Person? Get(int PersonId)
        {
            return _context.People
               .Include(x => x.Post)
               .Include(x => x.Department)
               .FirstOrDefault(x => x.Id == PersonId);
        }

        public List<Person> GetAll()
        {
            return _context.People
                .Include(x => x.Post)
                .Include(x => x.Department)
                .ToList();
        }

        public Person? GetMy(int userId)
        {
            return _context.People
                .Include(x => x.Post)
                .Include(x => x.Department)
                .First(x => x.Id == userId);
        }
        #endregion

    }
}
