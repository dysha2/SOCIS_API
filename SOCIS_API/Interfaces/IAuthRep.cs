namespace SOCIS_API.Interfaces
{
    public interface IAuthRep
    {
        public Person? ValidPerson(string userName, string password);
        public bool SetNewPassword(string userName, string password);
    }
}
