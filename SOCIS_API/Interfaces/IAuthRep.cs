namespace SOCIS_API.Interfaces
{
    public interface IAuthRep
    {
        public bool SetNewPassword(string userName, string password);
        public Person? ValidPerson(string userName, string password);
    }
}
