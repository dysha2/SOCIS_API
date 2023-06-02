using SOCIS_API.Model;

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
            var person= _context.People
               .Include(x => x.Post)
               .Include(x => x.Department)
               .FirstOrDefault(x => x.Id == PersonId);
            if (person is not null)
            {
                person.Post = person.Post is not null ? person.Post = new PostDTO(person.Post) : null;
                person.Department = person.Department is not null ? person.Department = new DepartmentDTO(person.Department) : null;
            }
            return person;
        }

        public List<Person> GetAll()
        {
            var people = _context.People
                .Include(x => x.Post)
                .Include(x=>x.Department)
                .ToList();
            if (people.Count > 0)
            {
                people.ForEach(x => x.Post = x.Post is not null ? x.Post = new PostDTO(x.Post) : null);
                people.ForEach(x => x.Department = x.Department is not null ? x.Department = new DepartmentDTO(x.Department) : null);
            }
            return people;
        }

        public Person? GetMy(int userId)
        {
            var person = _context.People
                .Include(x => x.Post)
                .Include(x => x.Department)
                .First(x => x.Id == userId);
            if (person is not null)
            {
                person.Post = person.Post is not null ? person.Post = new PostDTO(person.Post) : null;
                person.Department = person.Department is not null ? person.Department = new DepartmentDTO(person.Department) : null;
            }
            return person;
        }
        #endregion

    }
}
