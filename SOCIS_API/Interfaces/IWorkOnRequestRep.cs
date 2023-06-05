namespace SOCIS_API.Interfaces
{
    public interface IWorkOnRequestRep
    {
        public List<WorkOnRequest> GetByRequestAll(int reqId);
        public List<WorkOnRequest> GetByMyRequestAll(int reqId,int userId);
        public void AddMy(WorkOnRequest workOnRequest,int userId);
        public void UpdateMy(int wreqId,WorkOnRequest workOnRequest,int userId);

    }
}
