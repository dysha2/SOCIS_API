using HelpfulProjectCSharp;
using Microsoft.Data.SqlClient;

namespace SOCIS_API.Repositoryies
{
    public class AuthRep : IAuthRep
    {
        EquipmentContext _context;

        public AuthRep(EquipmentContext context)
        {
            _context = context;
        }

        public bool SetNewPassword(string userName, string password)
        {
            var person = _context.People.FirstOrDefault(x => x.UserName == userName);
            if (person is not null)
            {
                Random rnd = new Random();
                string salt = RegistrationTools.GetRandomKey(rnd.Next(128, 256));
                string pass = RegistrationTools.GetPasswordSha256(password, salt);
                person.Password = pass;
                person.PasswordSalt = salt;
                _context.SaveChanges();
                return true;
            } 
            else
                return false;
        }

        public Person? ValidPerson(string userName, string password)
        {
            var person = _context.People.Include(x=>x.Role).FirstOrDefault(x=>x.UserName== userName);
            if (person is not null)
            {
                if (person.Password == RegistrationTools.GetPasswordSha256(password, person.PasswordSalt))
                {
                    return person;
                } return null;
            }
            else
                return null;
        }
    }
}
