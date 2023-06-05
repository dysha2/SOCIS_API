namespace SOCIS_API.Repositoryies
{
    public class WorkOnRequestRep : IWorkOnRequestRep
    {
        private EquipmentContext _context;
        public WorkOnRequestRep(EquipmentContext equipmentContext)
        {
            _context = equipmentContext;
        }

        public void AddMy(WorkOnRequest workOnRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public WorkOnRequest Get(int id, int userId)
        {
            return _context.WorkOnRequests.FirstOrDefault(x => x.Id == id && x.Request.DeclarantId == userId);
        }

        public WorkOnRequest Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<WorkOnRequest> GetByRequestAll(int reqId)
        {
            throw new NotImplementedException();
        }

        public WorkOnRequest? GetMy(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public List<WorkOnRequest> GetMyAll(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMy(int wreqId, WorkOnRequest workOnRequest, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
