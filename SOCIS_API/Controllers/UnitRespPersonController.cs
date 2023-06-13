using HelpfulProjectCSharp.ASP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class UnitRespPersonController : Controller
    {
        private readonly ICrudRep crudRep;
        private readonly IUnitRespPersonRep _IUnitRespPersonRep;

        public UnitRespPersonController(ICrudRep iCrudRep, IUnitRespPersonRep iUnitRespPersonRep)
        {
            crudRep = iCrudRep;
            _IUnitRespPersonRep = iUnitRespPersonRep;
        }
        #region Get
        [HttpGet("Get/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult<UnitRespPerson> Get(int id)
        {
            UnitRespPerson UnitRespPerson = _IUnitRespPersonRep.Get(id);
            return UnitRespPerson is null ? NotFound() : UnitRespPerson;
        }
        [HttpGet("GetAllActiveByPerson/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult<List<UnitRespPerson>> GetAllActiveByPerson(int id)
        {
            return _IUnitRespPersonRep.GetAllActiveByPerson(id);
        }
        [HttpGet("GetAllOldByPerson/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult<List<UnitRespPerson>> GetAllOldByPerson(int id)
        {
            return _IUnitRespPersonRep.GetAllOldByPerson(id);
        }
        [HttpGet("GetAllByUnit/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult<List<UnitRespPerson>> GetAllByUnit(int id)
        {
            return _IUnitRespPersonRep.GetAllByUnit(id);
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin")]
        public IActionResult Add([FromBody] UnitRespPerson UnitRespPerson)
        {
            try
            {
                UnitRespPerson newUnitRespPerson = _IUnitRespPersonRep.Add(UnitRespPerson);
                if (newUnitRespPerson is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newUnitRespPerson.Id }, newUnitRespPerson);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] UnitRespPerson UnitRespPerson)
        {
            try
            {
                if (id != UnitRespPerson.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(UnitRespPerson);
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
                var UnitRespPerson = crudRep.Read<UnitRespPerson>(id);
                if (UnitRespPerson is null) return NotFound();
                crudRep.Delete(UnitRespPerson);
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
