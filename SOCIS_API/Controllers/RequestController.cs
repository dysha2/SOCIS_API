using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class RequestController : Controller
    {
        IRequestRep IRequestRep;

        public RequestController(IRequestRep iRequestRep)
        {
            IRequestRep = iRequestRep;
        }
        [HttpGet("GetMyAll"),Authorize]
        public IEnumerable<Request> GetMyRequests()
        {
            return IRequestRep.GetMyAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetMy"), Authorize]
        public Request GetMyRequest(int Id)
        {
            return IRequestRep.GetMy(Id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpPost("Add"),Authorize]
        public IActionResult AddRequest(int PlaceId, string Description)
        {
            try
            {
               IRequestRep.Create(PlaceId, Description, int.Parse(HttpContext.User.Claims.First(x=>x.Type=="Id").Value));
               return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message + "\r\n" + ex.InnerException.Message); }


        }
        [HttpPost("Update"), Authorize]
        public IActionResult UpdateRequest(int RequestId, string? Description, int? PlaceId, bool IsComplete)
        {
            try
            {
                IRequestRep.Update(RequestId, Description, PlaceId, IsComplete, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return Ok();
            }
            catch (Exception ex) {
                string ErrorInfo = ex.Message;
                if (ex.InnerException is not null) ErrorInfo = ErrorInfo + "\r\n" + ex.InnerException.Message;
                return BadRequest(ErrorInfo); 
            }
        }
    }
}
