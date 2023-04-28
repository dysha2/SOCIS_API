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
        public IEnumerable<Request> GetMyAll(int userId)
        {
            return _context.Requests
                .Where(x => x.DeclarantId == userId)
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .ToList();
        }
        public Request? GetMy(int id, int userId)
        {
            return _context.Requests
                .Where(x => x.DeclarantId == userId && x.Id==id)
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .FirstOrDefault();
        }
    }
}
