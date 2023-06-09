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

        #region Add
        public Person Add(Person person)
        {
            var per = new Person
            {
                Name=person.Name,
                Surname=person.Surname,
                Lastname=person.Lastname,
                PostId=person.PostId,
                RoleId=person.RoleId,
                DepartmentId=person.DepartmentId,
                Email=person.Email,
                Comment=person.Comment,
                UserName=person.UserName
            };
            _context.People.Add(per);
            _context.SaveChanges();
            return per;
        }
        #endregion

        #region Update
        public void Update(int PersonId,Person person)
        {
            Person newPerson = _context.People.Find(PersonId);
            newPerson.Name = person.Name;
            newPerson.Surname = person.Surname;
            newPerson.Lastname = person.Lastname;
            newPerson.PostId = person.PostId;
            newPerson.RoleId = person.RoleId;
            newPerson.DepartmentId = person.DepartmentId;
            newPerson.Email = person.Email;
            newPerson.Comment = person.Comment;
            newPerson.UserName = person.UserName;
            _context.SaveChanges();
        }
        #endregion

        #region Delete
        public void Delete(int PersonId)
        {
            Person person = _context.People.Find(PersonId);
            _context.People.Remove(person);
            _context.SaveChanges();
        }
        #endregion
    }
}
