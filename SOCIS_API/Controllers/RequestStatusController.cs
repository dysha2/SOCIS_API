using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class RequestStatusController : Controller
    {
        ICrudRep crudRep;

        public RequestStatusController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<RequestStatus>> GetAll()
        {
            return crudRep.Read<RequestStatus>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<RequestStatus> Get(int id)
        {
            RequestStatus RequestStatus = crudRep.Read<RequestStatus>(id);
            return RequestStatus is null ? NotFound() : RequestStatus;
        }
        #endregion

        #region RequestStatus
        [HttpPost("Add"), Authorize(Roles = "admin")]
        public IActionResult Add([FromBody] RequestStatus RequestStatus)
        {
            try
            {
                RequestStatus newRequestStatus = crudRep.Create(RequestStatus);
                if (newRequestStatus is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newRequestStatus.Id }, newRequestStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] RequestStatus RequestStatus)
        {
            try
            {
                if (id != RequestStatus.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(RequestStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
        #region Delete
        [HttpDelete("Delete/{id}"), Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                var RequestStatus = crudRep.Read<RequestStatus>(id);
                if (RequestStatus is null) return NotFound();
                crudRep.Delete(RequestStatus);
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
