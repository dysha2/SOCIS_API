using SOCIS_API.Model;

namespace SOCIS_API.Interfaces
{
    public interface IRequestRep
    {
        IEnumerable<Request> GetMyAll(int userId);
        Request GetMy(int RequestId, int userId);
        void Create(int PlaceId,string Description,int userId);
        void Update(int RequestId, string? Description, int? PlaceId, bool IsComplete, int userId);
    }
}
