namespace SOCIS_API.Interfaces
{
    public interface IWorkOnRequestRep
    {
        public List<WorkOnRequest> GetByRequestAll(int reqId);
        public List<WorkOnRequest> GetByMyRequestAll(int reqId,int userId);
        public WorkOnRequest? Get(int worId);
        public WorkOnRequest AddMy(WorkOnRequest workOnRequest,int userId);
        public WorkOnRequest AddMyAccepted(int reqId,int userId);
        public WorkOnRequest AddMyRefusal(int reqId, int userId);
        public WorkOnRequest AddMyComplete(int reqId,int userId);
        public void UpdateMy(int wreqId,WorkOnRequest workOnRequest,int userId);

    }
}
