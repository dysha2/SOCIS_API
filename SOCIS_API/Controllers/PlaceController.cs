﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class PlaceController : Controller
    {
        ICrudRep crudRep;

        public PlaceController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Place>> GetAll()
        {
            return crudRep.Read<Place>();
        }
        [HttpGet("Get/{id}"),Authorize]
        public ActionResult<Place> Get(int id)
        {
            Place place = crudRep.Read<Place>(id);
            return place is null ? NotFound() : place;
        }
        #endregion

        #region Post
        [HttpPost("Add"),Authorize(Roles ="admin,laborant")]
        public IActionResult Add([FromBody] Place place)
        {
            try
            {
                Place newPlace = crudRep.Create(place);
                if (newPlace is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newPlace.Id }, newPlace);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"),Authorize(Roles ="admin,laborant")]
        public IActionResult Update(int id,[FromBody]Place place)
        {
            try { 
                if (id != place.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(place);
                return NoContent();
            }
            catch (Exception ex) {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion
        #region Delete
        [HttpDelete("Delete/{id}"),Authorize(Roles ="admin,laborant")]
        public ActionResult Delete(int id)
        {
            try
            {
                var place = crudRep.Read<Place>(id);
                if (place is null) return NotFound();
                crudRep.Delete(place);
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
