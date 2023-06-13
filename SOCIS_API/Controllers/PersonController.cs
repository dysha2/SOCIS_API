using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOCIS_API.Model;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class PersonController : Controller
    {
        IPersonRep personRep;

        public PersonController(IPersonRep personRep)
        {
            this.personRep = personRep;
        }
        #region Get
        [HttpGet("Get/{id}"), Authorize(Roles = "admin,laborant")]
        public ActionResult<Person> Get(int id)
        {
            Person person = personRep.Get(id);
            return person is null ? NotFound() : person;
        }
        [HttpGet("GetMy"), Authorize]
        public ActionResult<Person> GetMy()
        {
            return personRep.GetMy(int.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value));
        }
        [HttpGet("GetAll"), Authorize(Roles = "admin,laborant")]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return personRep.GetAll();
        }
        [HttpGet("GetAllLaborants"),Authorize(Roles="admin")]
        public ActionResult<IEnumerable<Person>> GetAllLaborants()
        {
            return personRep.GetAllLaborants();
        }
        [HttpGet("GetRespPerson/{id}"),Authorize(Roles ="admin,laborants")]
        public ActionResult<Person> GetRespPerson(int id)
        {
            var person = personRep.GetRespPerson(id);
            return person == null ? NotFound() : person;
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin")]
        public IActionResult Update(int id, [FromBody] Person person)
        {
            try
            {
                if (person.Id != id)
                {
                    return BadRequest("Id not matched");
                }
                personRep.Update(id, person);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Add
        [HttpPost("Add"), Authorize(Roles = "admin")]
        public IActionResult Add([FromBody] Person person)
        {
            try
            {
                Person newPerson = personRep.Add(person);
                return CreatedAtAction(nameof(Get), new { id = newPerson.Id }, newPerson);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Delete
        [HttpDelete("Delete/{id}"),Authorize(Roles ="admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                personRep.Delete(id);
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
