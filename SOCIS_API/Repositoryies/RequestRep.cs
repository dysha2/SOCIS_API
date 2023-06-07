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
            var reqs = _context.Requests.Where(x => x.DeclarantId == userId);
            return ReqLoadData(reqs).ToList();
        }
        public List<Request> GetMyActiveAll(int userId)
        {
            var reqs = _context.Requests
                .Where(x => x.DeclarantId == userId && x.IsComplete == false);
            return ReqLoadData(reqs).ToList();
        }

        public List<Request> GetMyCompletedAll(int userId)
        {
            var reqs = _context.Requests
                .Where(x => x.DeclarantId == userId && x.IsComplete == true);
            return ReqLoadData(reqs).ToList();
        }
        public Request? GetMy(int id, int userId)
        {
            var reqs = _context.Requests.Where(x => x.DeclarantId == userId && x.Id == id);
            return ReqLoadData(reqs).FirstOrDefault();
        }
        public List<Request> GetAll()
        {
            var reqs = _context.Requests;
            return ReqLoadData(reqs).ToList();
        }

        public Request? Get(int RequestId)
        {
            var reqs = _context.Requests.Where(x => x.Id == RequestId);
            return ReqLoadData(reqs).FirstOrDefault();
        }
        public List<Request> GetMyByImpActiveAll(int userId)
        {
            var reqs = _context.Requests
                .Where(x => x.RequestStatus.Code == "accepted" && x.WorkOnRequests.Where(y => y.Service.Code == "accept").OrderBy(y => y.DateTime).LastOrDefault().ImplementerId == userId);
            return ReqLoadData(reqs).ToList();
        }
        public List<Request> GetByImpActiveAll(int personId)
        {
            var reqs = _context.Requests
                .Where(x => x.RequestStatus.Code == "accepted" && x.WorkOnRequests.Where(y => y.Service.Code == "accept").OrderBy(y => y.DateTime).LastOrDefault().ImplementerId == personId);
            return ReqLoadData(reqs).ToList();

        }
        public List<Request> GetMyByImpCompletedAll(int userId)
        {
            var reqs = _context.Requests
                .Where(x => x.RequestStatus.Code == "complete" && x.WorkOnRequests.Where(y => y.Service.Code == "accept").OrderBy(y => y.DateTime).LastOrDefault().ImplementerId == userId);
            return ReqLoadData(reqs).ToList();
        }
        public List<Request> GetActiveAll()
        {
            var reqs = _context.Requests.Where(x => x.RequestStatus.Code == "wait");
            return ReqLoadData(reqs).ToList();
        }
        private IQueryable<Request> ReqLoadData(IQueryable<Request> reqs)
        {
          return reqs.Include(x => x.Place)
                .Include(x => x.RequestStatus)
                .Include(x => x.Declarant)
                .Select(x => new RequestDTO(x)
                {
                    Declarant = new PersonDTO(x.Declarant),
                    Place = new PlaceDTO(x.Place),
                    RequestStatus = new RequestStatusDTO(x.RequestStatus)
                });
        }
        #endregion

        #region Add
        public Request AddMy(Request req, int userId)
        {
            Request newReq = new Request
            {
                DeclarantId = userId,
                Description = req.Description,
                PlaceId = req.PlaceId

            };
            _context.Requests.Add(newReq);
            _context.SaveChanges();
            return newReq;
        }
        public Request Add(Request req)
        {
            Request newReq = new Request
            {
                DeclarantId = req.DeclarantId,
                Description = req.Description,
                PlaceId = req.PlaceId
            };
            _context.Requests.Add(newReq);
            _context.SaveChanges();
            return newReq;
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
