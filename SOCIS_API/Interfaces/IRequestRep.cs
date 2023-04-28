using SOCIS_API.Model;

namespace SOCIS_API.Interfaces
{
    public interface IRequestRep
    {
        IEnumerable<Request> GetMyAll(int userId);
        Request? GetMy(int RequestId, int userId);
    }
}
