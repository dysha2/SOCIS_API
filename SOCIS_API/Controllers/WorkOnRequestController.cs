using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class WorkOnRequestController : Controller
    {
        private IWorkOnRequestRep IWorkOnRequestRep;
        private ICrudRep ICrudRep;

        public WorkOnRequestController(IWorkOnRequestRep iWorkOnRequestRep, ICrudRep iCrudRep)
        {
            IWorkOnRequestRep = iWorkOnRequestRep;
            ICrudRep = iCrudRep;
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
        [HttpGet("Get/{id}"), Authorize(Roles ="admin,laborant")]
        public ActionResult<WorkOnRequest> Get(int id)
        {
            var wor = IWorkOnRequestRep.Get(id);
            return wor is null?NotFound():wor;
        }
        #endregion

        #region Post
        [HttpPost("Add"),Authorize(Roles ="admin")]
        public IActionResult Add([FromBody] WorkOnRequest wor)
        {
            try
            {
                var newWor = ICrudRep.Create(wor);
                if (newWor is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newWor.Id }, newWor);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
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
        #region Put
        [HttpPut("UpdateMy/{id}"),Authorize(Roles ="admin,laborant")]
        public IActionResult UpdateMy(int id, [FromBody] WorkOnRequest wor)
        {
            try
            {
                if (id != wor.Id)
                {
                    return BadRequest("Id not matched");
                }
                IWorkOnRequestRep.UpdateMy(id,wor, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
    }
}
