namespace SOCIS_API.Interfaces
{
    public interface IFullNameUnitRep
    {
        FullNameUnit? Get(int id);
        List<FullNameUnit> GetAll();
    }
}
