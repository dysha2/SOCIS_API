using System.ComponentModel.DataAnnotations.Schema;
namespace SOCIS_API.Model
{
    public partial class Request
    {
        [NotMapped]
        public int CurrentImplementerId
        {
            get
            {
                if ((WorkOnRequests.Count > 0)&&(RequestStatus.Code!="wait"))
                {
                    var AcceptRecords = WorkOnRequests.Where(x => x.Service.Code == "accept");
                    return AcceptRecords.Where(x => x.DateTime == AcceptRecords.Max(x => x.DateTime)).First().ImplementerId;
                }
                else 
                    return 0;
            }
        }
    }
}
