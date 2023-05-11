using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SOCIS_API.Repositoryies
{
    public class RequestRep : IRequestRep
    {
        private EquipmentContext _context;
        public RequestRep(EquipmentContext equipmentContext)
        {
            _context = equipmentContext;
        }
        #region Get
        public List<Request> GetMyAll(int userId)
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
                .Where(x => x.DeclarantId == userId && x.Id == id)
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .FirstOrDefault();
        }
        public List<Request> GetAll()
        {
            return _context.Requests
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .ToList();
        }

        public Request? Get(int RequestId)
        {
            return _context.Requests
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .FirstOrDefault(x => x.Id == RequestId);
        }
        #endregion
        #region Add
        public void AddMy(Request req, int userId)
        {
            req.DeclarantId = userId;
            req.DateTimeStart = new DateTime();
            req.IsComplete = false;
            req.DateTimeEnd = null;
            _context.Requests.Add(req);
            _context.SaveChanges();

        }
        public void Add(Request req)
        {
            req.DateTimeStart = new DateTime();
            req.IsComplete = false;
            req.DateTimeEnd = null;
            _context.Requests.Add(req);
            _context.SaveChanges();
        }
        #endregion
        #region Update
        public void UpdateMy(int reqId,Request request, int userId)
        {
            Request updateReq = _context.Requests.Find(reqId);
            if (updateReq.IsComplete) throw new Exception("Request is complete. Update banned");
            if (updateReq.DeclarantId != userId) throw new Exception("Request declarant isn`t you. Update banned");
            updateReq.Description = request.Description;
            updateReq.PlaceId = request.PlaceId;
            updateReq.IsComplete = request.IsComplete;
            _context.SaveChanges();
        }
        #endregion
        #region Delete
        public void Delete(int reqId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
