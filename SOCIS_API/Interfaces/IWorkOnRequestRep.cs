namespace SOCIS_API.Interfaces
{
    public interface IWorkOnRequestRep
    {
        public WorkOnRequest? GetMy(int id,int userId);
        public WorkOnRequest Get(int id);
        public List<WorkOnRequest> GetByRequestAll(int reqId);
        public List<WorkOnRequest> GetMyAll(int userId);
        public void AddMy(WorkOnRequest workOnRequest,int userId);
        public void UpdateMy(int wreqId,WorkOnRequest workOnRequest,int userId);

    }
}
