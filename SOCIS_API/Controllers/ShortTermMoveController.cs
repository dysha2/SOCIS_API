using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class ShortTermMoveController : Controller
    {
        ICrudRep crudRep;
        IShortTermMoveRep ShortTermMoveRep;

        public ShortTermMoveController(ICrudRep crudRep, IShortTermMoveRep ShortTermMoveRep)
        {
            this.crudRep = crudRep;
            this.ShortTermMoveRep = ShortTermMoveRep;
        }
        #region Get
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<ShortTermMove> Get(int id)
        {
            var up = ShortTermMoveRep.Get(id);
            return up is null ? NotFound() : up;
        }
        [HttpGet("GetAllActiveByPlace/{id}"), Authorize]
        public ActionResult<List<ShortTermMove>> GetAllActiveByPlace(int id)
        {
            return ShortTermMoveRep.GetAllActiveByPlace(id);
        }
        [HttpGet("GetAllOldByPlace/{id}"), Authorize]
        public ActionResult<List<ShortTermMove>> GetAllOldByPlace(int id)
        {
            return ShortTermMoveRep.GetAllOldByPlace(id);
        }
        [HttpGet("GetAllByUnit/{id}"), Authorize]
        public ActionResult<List<ShortTermMove>> GetAllByUnit(int id)
        {
            return ShortTermMoveRep.GetAllByUnit(id);
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] ShortTermMove ShortTermMove)
        {
            try
            {
                ShortTermMove newShortTermMove = ShortTermMoveRep.Add(ShortTermMove);
                return CreatedAtAction(nameof(Get), new { Id = newShortTermMove.ShortTermMoveId }, newShortTermMove);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] ShortTermMove ShortTermMove)
        {
            try
            {
                if (id != ShortTermMove.ShortTermMoveId)
                {
                    return BadRequest("Id not matched");
                }
                return crudRep.Update(ShortTermMove) ? NoContent() : NotFound();

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
                var ShortTermMove = crudRep.Read<ShortTermMove>(id);
                if (ShortTermMove is null) return NotFound();
                crudRep.Delete(ShortTermMove);
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
