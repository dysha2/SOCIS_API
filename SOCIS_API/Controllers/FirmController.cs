using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class FirmController : Controller
    {
        ICrudRep crudRep;

        public FirmController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Firm>> GetAll()
        {
            return crudRep.Read<Firm>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<Firm> Get(int id)
        {
            Firm Firm = crudRep.Read<Firm>(id);
            return Firm is null ? NotFound() : Firm;
        }
        #endregion

        #region Firm
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] Firm Firm)
        {
            try
            {
                Firm newFirm = crudRep.Create(Firm);
                if (newFirm is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newFirm.Id }, newFirm);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] Firm Firm)
        {
            try
            {
                if (id != Firm.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(Firm);
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
                var Firm = crudRep.Read<Firm>(id);
                if (Firm is null) return NotFound();
                crudRep.Delete(Firm);
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
