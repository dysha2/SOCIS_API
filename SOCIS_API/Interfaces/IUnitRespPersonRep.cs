namespace SOCIS_API.Interfaces
{
    public interface IUnitRespPersonRep
    {
        UnitRespPerson? Get(int id);
        List<UnitRespPerson> GetAllActiveByPerson(int personId);
        List<UnitRespPerson> GetAllOldByPerson(int personId);
        List<UnitRespPerson> GetAllByUnit(int unitId);
        UnitRespPerson Add(UnitRespPerson upr);
        void Update(int uprId,UnitRespPerson upr);
    }
}
