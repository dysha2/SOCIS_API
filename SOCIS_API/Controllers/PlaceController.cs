using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("GetAll"),Authorize]
        public ActionResult<IEnumerable<Place>> GetAll()
        {
            return crudRep.Read<Place>();
        }
    }
}
