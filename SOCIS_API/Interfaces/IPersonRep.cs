namespace SOCIS_API.Interfaces
{
    public interface IPersonRep
    {
        List<Person> GetAll();
        List<Person> GetAllLaborants();
        Person? Get(int PersonId);
        Person? GetMy(int userId);
        void Update(int PersonId, Person person);
    }
}
