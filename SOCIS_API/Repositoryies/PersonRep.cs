using SOCIS_API.Model;
using System;

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
            var people = _context.People.Where(x=>x.Id==PersonId);
            return LoadData(people).FirstOrDefault();
        }

        public List<Person> GetAll()
        {
            var people = _context.People;
            return LoadData(people).ToList();
        }

        public List<Person> GetAllLaborants()
        {
            var people = _context.People.Where(x => x.Role.Name=="laborant");
            return LoadData(people).ToList();
        }

        public Person? GetMy(int userId)
        {
            var people = _context.People.Where(x => x.Id == userId);
            return LoadData(people).FirstOrDefault();
        }
        private IQueryable<Person> LoadData(IQueryable<Person> people)
        {
            return people.Include(x => x.Post)
                  .Include(x => x.Department)
                  .Select(x => new PersonDTO(x)
                  {
                      Post = x.Post != null ? new PostDTO(x.Post) : null,
                      Department = x.Department != null ? new DepartmentDTO(x.Department) : null
                  });
        }

    #endregion

    }
}
