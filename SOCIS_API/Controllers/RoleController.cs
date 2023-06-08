using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class RoleController : Controller
    {
        ICrudRep crudRep;

        public RoleController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Role>> GetAll()
        {
            return crudRep.Read<Role>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<Role> Get(int id)
        {
            Role Role = crudRep.Read<Role>(id);
            return Role is null ? NotFound() : Role;
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin")]
        public IActionResult Add([FromBody] Role Role)
        {
            try
            {
                Role newRole = crudRep.Create(Role);
                if (newRole is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newRole.Id }, newRole);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] Role Role)
        {
            try
            {
                if (id != Role.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(Role);
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
                var Role = crudRep.Read<Role>(id);
                if (Role is null) return NotFound();
                crudRep.Delete(Role);
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
