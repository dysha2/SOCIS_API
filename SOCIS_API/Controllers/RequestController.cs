using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class RequestController : Controller
    {
        IRequestRep IRequestRep;
        ICrudRep ICrudRep;

        public RequestController(IRequestRep iRequestRep, ICrudRep iCrudRep)
        {
            IRequestRep = iRequestRep;
            ICrudRep = iCrudRep;
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
                if (req is not null)
                {
                    req.DeclarantId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value);
                    bool result = ICrudRep.Create(req);
                    if (result)
                        return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        [HttpPut("Update/{id}"), Authorize]
        public IActionResult UpdateRequest(int id, [FromBody] Request newReq)
        {
            try
            {
                if (newReq is not null)
                {
                    var oldReq = IRequestRep.GetMy(id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                    if (oldReq is not null)
                    {
                        if (oldReq.DeclarantId == int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value))
                        {
                            if (oldReq.DateTimeEnd is null)
                            {
                                oldReq.DateTimeEnd = newReq.DateTimeEnd;
                                oldReq.Description = newReq.Description;
                                oldReq.PlaceId = newReq.PlaceId;
                                ICrudRep.Update(oldReq);
                                return Ok();
                            }
                            else
                            {
                                return BadRequest("Request is done yet. Update banned");
                            }
                        }
                        else
                        {
                            return BadRequest("Your can`t change declarant");
                        }
                    }
                    else
                    {
                        return BadRequest("You can`t change another request");
                    }
                }
                return BadRequest("Enter data");
            }
            catch (Exception ex) {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
    }
}
