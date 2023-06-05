using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class WorkOnRequestController : Controller
    {
        private IWorkOnRequestRep IWorkOnRequestRep;

        public WorkOnRequestController(IWorkOnRequestRep iWorkOnRequestRep)
        {
            IWorkOnRequestRep = iWorkOnRequestRep;
        }
        #region Get
        [HttpGet("GetByRequestAll/{reqId}"),Authorize(Roles ="admin,laborant")]
        public ActionResult<IEnumerable<WorkOnRequest>> GetByRequestAll(int reqId)
        {
            return IWorkOnRequestRep.GetByRequestAll(reqId);
        }
        [HttpGet("GetByMyRequestAll/{reqId}"), Authorize]
        public ActionResult<IEnumerable<WorkOnRequest>> GetByMyRequestAll(int reqId)
        {
            return IWorkOnRequestRep.GetByMyRequestAll(reqId, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        #endregion

        #region Post
        [HttpPost("AddMy"),Authorize(Roles="admin,laborant")]
        public IActionResult AddMy([FromBody] WorkOnRequest wor)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
    }
}
