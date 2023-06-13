namespace SOCIS_API.Interfaces
{
    public interface IShortTermMoveRep
    {
        ShortTermMove? Get(int id);
        List<ShortTermMove> GetAllActiveByPlace(int placeId);
        List<ShortTermMove> GetAllOldByPlace(int placeId);
        List<ShortTermMove> GetAllByUnit(int unitId);
        ShortTermMove Add(ShortTermMove ShortTermMove);
    }
}
