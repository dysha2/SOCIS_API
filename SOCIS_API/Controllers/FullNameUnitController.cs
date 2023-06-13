using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class FullNameUnitController : Controller
    {
        ICrudRep crudRep;
        IFullNameUnitRep fullNameUnitRep;

        public FullNameUnitController(ICrudRep crudRep, IFullNameUnitRep fullNameUnitRep)
        {
            this.crudRep = crudRep;
            this.fullNameUnitRep = fullNameUnitRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<FullNameUnit>> GetAll()
        {
            return fullNameUnitRep.GetAll();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<FullNameUnit> Get(int id)
        {
            FullNameUnit FullNameUnit = fullNameUnitRep.Get(id);
            return FullNameUnit is null ? NotFound() : FullNameUnit;
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] FullNameUnit FullNameUnit)
        {
            try
            {
                FullNameUnit newFullNameUnit = crudRep.Create(FullNameUnit);
                if (newFullNameUnit is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newFullNameUnit.Id }, newFullNameUnit);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] FullNameUnit FullNameUnit)
        {
            try
            {
                if (id != FullNameUnit.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(FullNameUnit);
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
                var FullNameUnit = crudRep.Read<FullNameUnit>(id);
                if (FullNameUnit is null) return NotFound();
                crudRep.Delete(FullNameUnit);
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
