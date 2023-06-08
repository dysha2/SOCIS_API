using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class DepartmentController : Controller
    {
        ICrudRep crudRep;

        public DepartmentController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Department>> GetAll()
        {
            return crudRep.Read<Department>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<Department> Get(int id)
        {
            Department dep = crudRep.Read<Department>(id);
            return dep is null ? NotFound() : dep;
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] Department department)
        {
            try
            {
                Department newDep = crudRep.Create(department);
                if (newDep is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newDep.Id }, newDep);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] Department department)
        {
            try
            {
                if (id != department.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(department);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
        #region Delete
        [HttpDelete("Delete/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult Delete(int id)
        {
            try
            {
                var department = crudRep.Read<Department>(id);
                if (department is null) return NotFound();
                crudRep.Delete(department);
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
