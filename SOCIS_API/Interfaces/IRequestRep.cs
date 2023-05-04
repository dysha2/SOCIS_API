using SOCIS_API.Model;

namespace SOCIS_API.Interfaces
{
    public interface IRequestRep
    {
        IEnumerable<Request> GetAll();
        Request? Get(int RequestId);
        IEnumerable<Request> GetMyAll(int userId);
        Request? GetMy(int RequestId, int userId);
        void Add(Request req, int userId);
        void Update(int reqId,Request request,int userId);
        void Delete(int reqId);
    }
}
