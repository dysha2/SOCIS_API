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
        #region Get
        [HttpGet("GetMyAll"),Authorize]
        public ActionResult<IEnumerable<Request>> GetMyAll()
        {
            return IRequestRep.GetMyAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetMyActiveAll"), Authorize]
        public ActionResult<IEnumerable<Request>> GetMyActiveAll()
        {
            return IRequestRep.GetMyActiveAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetMyCompletedAll"), Authorize]
        public ActionResult<IEnumerable<Request>> GetMyCompletedAll()
        {
            return IRequestRep.GetMyCompletedAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetMy/{id}"), Authorize]
        public ActionResult<Request> GetMy(int id)
        {
            var req = IRequestRep.GetMy(id, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
            if (req is null) return NotFound();
            return req;
        }
        [HttpGet("GetMyByImpActiveAll"), Authorize(Roles = "admin,laborant")]
        public ActionResult<IEnumerable<Request>> GetMyByImpActiveAll()
        {
            return IRequestRep.GetMyByImpActiveAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetByImpActiveAll"), Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<Request>> GetByImpActiveAll()
        {
            return IRequestRep.GetByImpActiveAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetMyByImpCompletedAll"), Authorize(Roles = "admin,laborant")]
        public ActionResult<IEnumerable<Request>> GetMyByImpCompletedAll()
        {
            return IRequestRep.GetMyByImpCompletedAll(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("Get/{id}"),Authorize(Roles ="admin,laborant")]
        public ActionResult<Request> Get(int id)
        {
            var req = IRequestRep.Get(id);
            if (req is null) return NotFound();
            return req;
        }
        [HttpGet("GetAll"), Authorize(Roles = "admin,laborant")]
        public ActionResult<IEnumerable<Request>> GetAll()
        {
            return IRequestRep.GetAll();
        }
        #endregion

        #region Post
        [HttpPost("AddMy"),Authorize]
        public IActionResult AddMy([FromBody]Request req)
        {
            try
            {
                IRequestRep.AddMy(req, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return CreatedAtAction(nameof(GetMy), new { Id = req.Id }, req);
            } 
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }

        }
        [HttpPost("Add"), Authorize(Roles ="admin,laborant")]
        public IActionResult Add([FromBody] Request req)
        {
            try
            {
                IRequestRep.Add(req);
                return CreatedAtAction(nameof(Get), new { Id = req.Id }, req);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
        #region Put
        [HttpPut("UpdateMy/{id}"), Authorize]
        public IActionResult UpdateMy(int id, [FromBody] Request newReq)
        {
            try
            {
                IRequestRep.UpdateMy(id,newReq, int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
                return NoContent();
            }
            catch (Exception ex) {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
        #region Delete
        #endregion
    }
}
