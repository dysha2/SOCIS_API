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
        public void AddMy(WorkOnRequest workOnRequest, int userId)
        {
            WorkOnRequest newWOR = new WorkOnRequest
            {
                ImplementerId=userId,
                RequestId=workOnRequest.RequestId,
                ServiceId=workOnRequest.ServiceId,
                Comment=workOnRequest.Comment
            };
            _context.WorkOnRequests.Add(newWOR);
            _context.SaveChanges();
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
