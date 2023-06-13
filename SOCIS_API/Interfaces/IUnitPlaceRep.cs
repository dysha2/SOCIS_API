namespace SOCIS_API.Interfaces
{
    public interface IUnitPlaceRep
    {
        UnitPlace? Get(int id);
        List<UnitPlace> GetAllActiveByPlace(int placeId);
        List<UnitPlace> GetAllOldByPlace(int placeId);
        List<UnitPlace> GetAllByUnit(int unitId);
        UnitPlace Add(UnitPlace unitPlace);
    }
}
