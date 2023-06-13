using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOCIS_API.Model;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class UnitPlaceController : Controller
    {
        ICrudRep crudRep;
        IUnitPlaceRep unitPlaceRep;

        public UnitPlaceController(ICrudRep crudRep,IUnitPlaceRep unitPlaceRep)
        {
            this.crudRep = crudRep;
            this.unitPlaceRep = unitPlaceRep;
        }
        #region Get
        [HttpGet("Get/{id}"),Authorize]
        public ActionResult<UnitPlace> Get(int id)
        {
            var up = unitPlaceRep.Get(id);
            return up is null ? NotFound() : up;
        }
        [HttpGet("GetAllActiveByPlace/{id}"),Authorize]
        public ActionResult<List<UnitPlace>> GetAllActiveByPlace(int id)
        {
            return unitPlaceRep.GetAllActiveByPlace(id);
        }
        [HttpGet("GetAllOldByPlace/{id}"), Authorize]
        public ActionResult<List<UnitPlace>> GetAllOldByPlace(int id)
        {
            return unitPlaceRep.GetAllOldByPlace(id);
        }
        [HttpGet("GetAllByUnit/{id}"), Authorize]
        public ActionResult<List<UnitPlace>> GetAllByUnit(int id)
        {
            return unitPlaceRep.GetAllByUnit(id);
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] UnitPlace UnitPlace)
        {
            try
            {
                UnitPlace newUnitPlace = unitPlaceRep.Add(UnitPlace);
                return CreatedAtAction(nameof(Get), new { Id = newUnitPlace.Id }, newUnitPlace);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] UnitPlace UnitPlace)
        {
            try
            {
                if (id != UnitPlace.Id)
                {
                    return BadRequest("Id not matched");
                }
                return crudRep.Update(UnitPlace) ? NoContent() : NotFound(); 

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
                var UnitPlace = crudRep.Read<UnitPlace>(id);
                if (UnitPlace is null) return NotFound();
                crudRep.Delete(UnitPlace);
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
