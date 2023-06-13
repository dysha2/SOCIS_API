using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class PlaceController : Controller
    {
        ICrudRep crudRep;
        IPlaceRep placeRep;

        public PlaceController(ICrudRep crudRep, IPlaceRep placeRep)
        {
            this.crudRep = crudRep;
            this.placeRep = placeRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Place>> GetAll()
        {
            return crudRep.Read<Place>();
        }
        [HttpGet("GetCurrentPlace/{id}")]
        public ActionResult<Place> GetCurrentPlace(int id)
        {
            var place = placeRep.CurrentPlace(id);
            return place is null ? NotFound() : place;
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
                return crudRep.Update(place) ? NoContent() : NotFound();

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
