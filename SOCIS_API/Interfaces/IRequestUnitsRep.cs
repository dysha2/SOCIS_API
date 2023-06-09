namespace SOCIS_API.Interfaces
{
    public interface IRequestUnitsRep
    {
        public RequestUnit? Get(int ReqUnId);
        public List<RequestUnit> GetAllByWorkOnRequest(int worId);
        public RequestUnit AddMy(RequestUnit requestUnit,int userId);
        public void UpdateMy(int ReqUnId,RequestUnit requestUnit,int userId);
        public void DeleteMy(int ReqUnId, int userId);
    }
}
