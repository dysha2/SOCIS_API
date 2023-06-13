namespace SOCIS_API.Repositoryies
{
    public class PlaceRep : IPlaceRep
    {
        private readonly EquipmentContext _context;

        public PlaceRep(EquipmentContext context)
        {
            _context = context;
        }

        public Place? CurrentPlace(int unitId)
        {
            var place = _context.ShortTermMoves.Where(x => x.UnitId == unitId && x.DateTimeEndFact == null).Select(x=>x.Place).FirstOrDefault();
            if (place is null)
                return _context.UnitPlaces.Where(x => x.UnitId == unitId && x.DateEnd == null).Select(x => x.Place).FirstOrDefault();
            else
                return place;
        }
    }
}
