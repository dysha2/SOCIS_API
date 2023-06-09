using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class UnitTypeController : Controller
    {
        ICrudRep crudRep;

        public UnitTypeController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<UnitType>> GetAll()
        {
            return crudRep.Read<UnitType>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<UnitType> Get(int id)
        {
            UnitType UnitType = crudRep.Read<UnitType>(id);
            return UnitType is null ? NotFound() : UnitType;
        }
        #endregion

        #region UnitType
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] UnitType UnitType)
        {
            try
            {
                UnitType newUnitType = crudRep.Create(UnitType);
                if (newUnitType is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newUnitType.Id }, newUnitType);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] UnitType UnitType)
        {
            try
            {
                if (id != UnitType.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(UnitType);
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
                var UnitType = crudRep.Read<UnitType>(id);
                if (UnitType is null) return NotFound();
                crudRep.Delete(UnitType);
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
