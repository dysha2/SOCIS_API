using SOCIS_API.Model;

namespace SOCIS_API.Repositoryies
{
    public class WorkOnRequestRep : IWorkOnRequestRep
    {
        private EquipmentContext _context;
        public WorkOnRequestRep(EquipmentContext equipmentContext)
        {
            _context = equipmentContext;
        }
        #region Get

        public List<WorkOnRequest> GetByRequestAll(int reqId)
        {
            var wors = _context.WorkOnRequests.Where(x => x.RequestId == reqId);
            return LoadData(wors).ToList();
        }
        public List<WorkOnRequest> GetByMyRequestAll(int reqId,int userId)
        {
            var wors = _context.WorkOnRequests.Where(x => x.RequestId == reqId && x.Request.DeclarantId==userId);
            return LoadData(wors).ToList();
        }
        public WorkOnRequest? Get(int worId)
        {
            var wors = _context.WorkOnRequests.Where(x => x.Id == worId);
            return LoadData(wors).FirstOrDefault();
        }
        private IQueryable<WorkOnRequest> LoadData(IQueryable<WorkOnRequest> wors)
        {
            return wors
                .Include(x => x.Request)
                .Include(x => x.Service)
                .Include(x => x.Implementer)
                .Select(x => new WorkOnRequestDTO(x)
                {
                    //Request=new RequestDTO(x.Request),
                    Service=new ServiceDTO(x.Service),
                    Implementer=new PersonDTO(x.Implementer)
                });
        }
        #endregion

        #region Add
        public WorkOnRequest AddMy(WorkOnRequest workOnRequest, int userId)
        {
            Request req = _context.Requests.Include(x => x.RequestStatus).FirstOrDefault(x => x.Id == workOnRequest.RequestId);
            if (req is null) throw new Exception("RequestId is required field");
            if (req.RequestStatus.Code == "wait") throw new Exception("Request is not accepted. You can`t add work on request");
            if (req.IsComplete == true) throw new Exception("Request is complete. You can`t add work on request");
            Service service = _context.Services.Find(workOnRequest.ServiceId);
            if ((service.Code == "accept") || (service.Code == "refusal")) throw new Exception("You can`t execute this service in this method");
            WorkOnRequest newWOR = new WorkOnRequest
            {
                ImplementerId=userId,
                RequestId=workOnRequest.RequestId,
                ServiceId=workOnRequest.ServiceId,
                Comment=workOnRequest.Comment
            };
            _context.WorkOnRequests.Add(newWOR);
            _context.SaveChanges();
            return newWOR;
        }
        public WorkOnRequest AddMyAccepted(int reqId, int userId)
        {
            Request req = _context.Requests.Include(x => x.RequestStatus).FirstOrDefault(x => x.Id == reqId);
            if(req is null) throw new Exception("Request not found");
            if (req.RequestStatus.Code!="wait") throw new Exception("You can`t accept this request");
            WorkOnRequest wor = new WorkOnRequest
            {
                RequestId = reqId,
                ServiceId = _context.Services.First(x => x.Code == "accept").Id,
                ImplementerId=userId
            };
            _context.WorkOnRequests.Add(wor);
            req.RequestStatusId = _context.RequestStatuses.First(x => x.Code == "accepted").Id;
            _context.SaveChanges();
            return wor;
        }

        public WorkOnRequest AddMyRefusal(int reqId, int userId)
        {
            Request req = _context.Requests
                .Include(x => x.WorkOnRequests)
                .ThenInclude(x => x.Service)
                .Where(x => x.Id == reqId)
                .FirstOrDefault();
            if (req is null) throw new Exception("Request not found");
            if (req.WorkOnRequests.Where(y => y.Service.Code == "accept").OrderBy(y => y.DateTime).LastOrDefault().ImplementerId!=userId) 
                throw new Exception("You can`t refusal this request");
            WorkOnRequest wor = new WorkOnRequest
            {
                RequestId = reqId,
                ServiceId = _context.Services.First(x => x.Code == "refusal").Id,
                ImplementerId = userId
            };
            _context.WorkOnRequests.Add(wor);
            req.RequestStatusId = _context.RequestStatuses.First(x => x.Code == "wait").Id;
            _context.SaveChanges();
            return wor;
        }
        public WorkOnRequest AddMyComplete(int reqId,int userId)
        {
            Request req = _context.Requests
                .Include(x => x.WorkOnRequests)
                .ThenInclude(x => x.Service)
                .Where(x => x.Id == reqId)
                .FirstOrDefault();
            if (req is null) throw new Exception("Request not found");
            if (req.WorkOnRequests.Where(y => y.Service.Code == "accept").OrderBy(y => y.DateTime).LastOrDefault().ImplementerId != userId)
                throw new Exception("You can`t complete this request");
            WorkOnRequest wor = new WorkOnRequest
            {
                RequestId = reqId,
                ServiceId = _context.Services.First(x => x.Code == "complete").Id,
                ImplementerId = userId
            };
            _context.WorkOnRequests.Add(wor);
            req.IsComplete = true;
            _context.SaveChanges();
            return wor;
        }
        #endregion
        #region Update
        public void UpdateMy(int wreqId, WorkOnRequest workOnRequest, int userId)
        {
            throw new NotImplementedException();
        }

        


        #endregion
        #region Delete

        #endregion
    }
}
