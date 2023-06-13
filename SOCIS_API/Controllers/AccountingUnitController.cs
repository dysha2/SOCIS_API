using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class AccountingUnitController : Controller
    {
        ICrudRep crudRep;
        IAccountingUnitRep accountingUnitRep;

        public AccountingUnitController(ICrudRep crudRep, IAccountingUnitRep accountingUnitRep)
        {
            this.crudRep = crudRep;
            this.accountingUnitRep = accountingUnitRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<AccountingUnit>> GetAll()
        {
            return accountingUnitRep.GetAll();
        }
        [HttpGet("GetAllByFullNameUnit/{id}"), Authorize]
        public ActionResult<IEnumerable<AccountingUnit>> GetAllByFullNameUnit(int id)
        {
            return accountingUnitRep.GetAllByFullNameUnit(id);
        }
        //[HttpGet("GetAllByPlace/{id}"), Authorize]
        //public ActionResult<IEnumerable<AccountingUnit>> GetAllByPlace(int id)
        //{
        //    return accountingUnitRep.GetAllByPlace(id);
        //}
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<AccountingUnit> Get(int id)
        {
            AccountingUnit AccountingUnit = accountingUnitRep.Get(id);
            return AccountingUnit is null ? NotFound() : AccountingUnit;
        }
        #endregion

        #region AccountingUnit
        [HttpPost("Add"), Authorize(Roles = "admin")]
        public IActionResult Add([FromBody] AccountingUnit AccountingUnit)
        {
            try
            {
                AccountingUnit newAccountingUnit = crudRep.Create(AccountingUnit);
                if (newAccountingUnit is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newAccountingUnit.Id }, newAccountingUnit);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] AccountingUnit AccountingUnit)
        {
            try
            {
                if (id != AccountingUnit.Id)
                {
                    return BadRequest("Id not matched");
                }
                return crudRep.Update(AccountingUnit) ? NoContent() : NotFound();
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
                var AccountingUnit = crudRep.Read<AccountingUnit>(id);
                if (AccountingUnit is null) return NotFound();
                crudRep.Delete(AccountingUnit);
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
