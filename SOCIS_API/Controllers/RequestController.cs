using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;

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
        [HttpGet("GetMy/{id}"), Authorize]
        public ActionResult<Request> GetMyRequest(int Id)
        {
            var req = IRequestRep.GetMy(Id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
            if (req is null) return NotFound();
            return req;
        }
        [HttpPost("Add"),Authorize]
        public IActionResult AddRequest([FromBody]Request req)
        {
            try
            {
                IRequestRep.Add(req, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(GetMyRequest), new { Id = req.Id }, req);
            } catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }

        }
        [HttpPut("Update/{id}"), Authorize]
        public IActionResult UpdateRequest(int id, [FromBody] Request newReq)
        {
            try
            {
                IRequestRep.Update(id,newReq, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return NoContent();
            }
            catch (Exception ex) {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
    }
}
