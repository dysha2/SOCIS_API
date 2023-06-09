using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class RequestUnitsController : Controller
    {
        IRequestUnitsRep IRequestUnitsRep;

        public RequestUnitsController(IRequestUnitsRep ReqUnRep)
        {
            this.IRequestUnitsRep = ReqUnRep;
        }
        #region Get
        [HttpGet("GetAllByWorkOnRequest/{id}"), Authorize]
        public ActionResult<IEnumerable<RequestUnit>> GetAllByWorkOnRequest(int id)
        {
            return IRequestUnitsRep.GetAllByWorkOnRequest(id);
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<RequestUnit> Get(int id)
        {
            var ReqUn = IRequestUnitsRep.Get(id);
            return ReqUn is null ? NotFound() : ReqUn;
        }
        #endregion

        #region Post
        [HttpPost("AddMy"), Authorize(Roles = "admin,laborant")]
        public IActionResult AddMy([FromBody] RequestUnit ReqUn)
        {
            try
            {
                var newReqUn = IRequestUnitsRep.AddMy(ReqUn, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(Get), new { Id = newReqUn.Id }, newReqUn);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("UpdateMy/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] RequestUnit ReqUn)
        {
            try
            {
                if (id != ReqUn.Id)
                {
                    return BadRequest("Id not matched");
                }
                IRequestUnitsRep.UpdateMy(id, ReqUn, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
        #region Delete
        [HttpDelete("DeleteMy/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult Delete(int id)
        {
            try
            {
                IRequestUnitsRep.DeleteMy(id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
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
