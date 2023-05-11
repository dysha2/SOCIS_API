using SOCIS_API.Model;

namespace SOCIS_API.Interfaces
{
    public interface IRequestRep
    {
        List<Request> GetAll();
        Request? Get(int RequestId);
        List<Request> GetMyAll(int userId);
        Request? GetMy(int RequestId, int userId);
        void AddMy(Request req, int userId);
        void UpdateMy(int reqId,Request request,int userId);
        void Delete(int reqId);
        void Add(Request req);
    }
}
