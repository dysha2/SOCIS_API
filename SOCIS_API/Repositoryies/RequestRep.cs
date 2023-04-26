using Microsoft.EntityFrameworkCore;

namespace SOCIS_API.Repositoryies
{
    public class RequestRep : IRequestRep
    {
        private EquipmentContext _context;
        public RequestRep(EquipmentContext equipmentContext)
        {
            _context = equipmentContext;
        }
        public void Create(int PlaceId, string Description, int userId)
        {
            _context.Requests.Add(new Request() { PlaceId = PlaceId, Description = Description, DeclarantId = userId });
            _context.SaveChanges();
        }
        public IEnumerable<Request> GetMyAll(int userId)
        {
            return _context.Requests
                .Where(x => x.DeclarantId == userId)
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .ToList();
        }
        public Request GetMy(int id, int userId)
        {
            return _context.Requests
                .Where(x => x.DeclarantId == userId)
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .First();
        }
        public void Update(int RequestId, string? Description, int? PlaceId, bool IsComplete, int userId)
        {
            Request req = _context.Requests.FirstOrDefault(x => x.Id == RequestId && x.DeclarantId == userId);
            if (req == null) throw new Exception("Заявки не найдено");
            else
            {
                if (req.IsComplete == true) throw new Exception("Заявка завершена и не поддаётся редактированию");
                else
                {
                    if (!String.IsNullOrEmpty(Description)) req.Description = Description;
                    if (PlaceId is not null) req.PlaceId = PlaceId.Value;
                    req.IsComplete = IsComplete;
                    _context.SaveChanges();
                }
            }
        }
    }
}
