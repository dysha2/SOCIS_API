using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class ServiceController : Controller
    {
        ICrudRep crudRep;

        public ServiceController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Service>> GetAll()
        {
            return crudRep.Read<Service>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<Service> Get(int id)
        {
            Service Service = crudRep.Read<Service>(id);
            return Service is null ? NotFound() : Service;
        }
        #endregion

        #region Service
        [HttpPost("Add"), Authorize(Roles = "admin")]
        public IActionResult Add([FromBody] Service Service)
        {
            try
            {
                Service newService = crudRep.Create(Service);
                if (newService is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newService.Id }, newService);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] Service Service)
        {
            try
            {
                if (id != Service.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(Service);
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
                var Service = crudRep.Read<Service>(id);
                if (Service is null) return NotFound();
                crudRep.Delete(Service);
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
