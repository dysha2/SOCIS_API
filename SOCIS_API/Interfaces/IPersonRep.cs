namespace SOCIS_API.Interfaces
{
    public interface IPersonRep
    {
        List<Person> GetAll();
        Person? Get(int PersonId);
        Person? GetMy(int userId);
    }
}
