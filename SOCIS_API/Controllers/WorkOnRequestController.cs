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
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<WorkOnRequest> Get(int id)
        {
            var wor = IWorkOnRequestRep.Get(id);
            if (wor is null) return NotFound();
            return wor;
        }
        #endregion

        #region Post
        [HttpPost("AddMy"),Authorize(Roles="admin,laborant")]
        public IActionResult AddMy([FromBody] WorkOnRequest wor)
        {
            try
            {
                var newWOR=IWorkOnRequestRep.AddMy(wor, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(Get), new { Id=newWOR.Id}, newWOR);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        [HttpPost("AddMyAccepted/{reqId}"),Authorize(Roles ="admin,laborant")]
        public IActionResult AddMyAccepted(int reqId)
        {
            try
            {
                var newWOR = IWorkOnRequestRep.AddMyAccepted(reqId, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(Get), new { Id = newWOR.Id }, newWOR);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        [HttpPost("AddMyRefusal/{reqId}"), Authorize(Roles = "admin,laborant")]
        public IActionResult AddMyRefusal(int reqId)
        {
            try
            {
                var newWOR = IWorkOnRequestRep.AddMyRefusal(reqId, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(Get), new { Id = newWOR.Id }, newWOR);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        [HttpPost("AddMyComplete/{reqId}"), Authorize(Roles = "admin,laborant")]
        public IActionResult AddMyComplete(int reqId)
        {
            try
            {
                var newWOR = IWorkOnRequestRep.AddMyComplete(reqId, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(Get), new { Id = newWOR.Id }, newWOR);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
    }
}
