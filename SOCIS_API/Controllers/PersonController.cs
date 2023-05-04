﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class PersonController : Controller
    {
        ICrudRep crudRep;
        IPersonRep personRep;

        public PersonController(ICrudRep crudRep, IPersonRep personRep)
        {
            this.personRep = personRep;
            this.crudRep = crudRep;
        }
        [HttpGet("Get/{id}"), Authorize(Roles ="admin,laborant")]
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
        [HttpGet("GetAll"), Authorize(Roles ="admin,laborant")]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return personRep.GetAll();
        }

        [HttpPut("Update/{id}"),Authorize(Roles ="admin")]
        public IActionResult Update(int id,[FromBody] Person person)
        {
            try
            {
                if (id != person.Id) return BadRequest("Id not matched");
                crudRep.Update(person);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }

        [HttpPost("Add"),Authorize(Roles="admin")]
        public IActionResult Add([FromBody] Person person)
        {
            try
            {
                crudRep.Create(person);
                return CreatedAtAction(nameof(Get),new {id=person.Id},person);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
    }
}