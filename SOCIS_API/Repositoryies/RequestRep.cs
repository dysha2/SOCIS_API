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
            var reqs= _context.Requests
                .Where(x => x.DeclarantId == userId)
                .Include(x => x.Place)
                .Include(x=>x.RequestStatus)
                .ToList();
            ReqLoadData(reqs);
            return reqs;
        }
        public List<Request> GetMyActiveAll(int userId)
        {
            var reqs = _context.Requests
                .Where(x => x.DeclarantId == userId && x.IsComplete==false)
                .Include(x => x.Place)
                .Include(x => x.RequestStatus)
                .ToList();
            ReqLoadData(reqs);
            return reqs;
        }

        public List<Request> GetMyCompletedAll(int userId)
        {
            var reqs = _context.Requests
                .Where(x => x.DeclarantId == userId && x.IsComplete == true)
                .Include(x => x.Place)
                .Include(x => x.RequestStatus)
                .ToList();
            ReqLoadData(reqs);
            return reqs;
        }
        public Request? GetMy(int id, int userId)
        {
            var req= _context.Requests
                .Where(x => x.DeclarantId == userId && x.Id == id)
                .Include(x => x.Place)
                .Include(x=>x.RequestStatus)
                .Include(x => x.WorkOnRequests)
                .FirstOrDefault();
            ReqLoadData(req);
            return req;
        }
        public List<Request> GetAll()
        {
           var reqs= _context.Requests
                .Include(x => x.Place)
                .Include(x => x.RequestStatus)
                .ToList();
            ReqLoadData(reqs);
            return reqs;
        }

        public Request? Get(int RequestId)
        {
            var req= _context.Requests
                .Include(x => x.Place)
                .Include(x => x.WorkOnRequests)
                .FirstOrDefault(x => x.Id == RequestId);
            ReqLoadData(req);
            return req;
        }
        public List<Request> GetMyByImpActiveAll(int userId)
        {
            var reqs = _context.Requests
                .Include(x => x.Place)
                .Include(x => x.RequestStatus)
                .Where(x => x.IsComplete == false && x.CurrentImplementerId == userId)
                .ToList();
            ReqLoadData(reqs);
            return reqs;

        }
        public List<Request> GetMyByImpCompletedAll(int userId)
        {
            var reqs = _context.Requests
                .Include(x => x.Place)
                .Include(x => x.RequestStatus)
                .Where(x => x.IsComplete == true && x.CurrentImplementerId == userId)
                .ToList();
            ReqLoadData(reqs);
            return reqs;
        }
        private void ReqLoadData(Request? req)
        {
            if (req is not null)
            {
                req.Place = new PlaceDTO(req.Place);
                req.RequestStatus = new RequestStatusDTO(req.RequestStatus);
            }
        }
        private void ReqLoadData(List<Request> reqs)
        {
            if (reqs.Count > 0)
            {
                reqs.ForEach(x => x.Place = new PlaceDTO(x.Place));
                reqs.ForEach(x => x.RequestStatus = new RequestStatusDTO(x.RequestStatus));
            }
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
