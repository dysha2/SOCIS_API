using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SOCIS_API.Controllers
{
    [Route("crud/[controller]")]
    public class PostController : Controller
    {
        ICrudRep crudRep;

        public PostController(ICrudRep crudRep)
        {
            this.crudRep = crudRep;
        }
        #region Get
        [HttpGet("GetAll"), Authorize]
        public ActionResult<IEnumerable<Post>> GetAll()
        {
            return crudRep.Read<Post>();
        }
        [HttpGet("Get/{id}"), Authorize]
        public ActionResult<Post> Get(int id)
        {
            Post post = crudRep.Read<Post>(id);
            return post is null? NotFound() : post;
        }
        #endregion

        #region Post
        [HttpPost("Add"), Authorize(Roles = "admin,laborant")]
        public IActionResult Add([FromBody] Post Post)
        {
            try
            {
                Post newPost = crudRep.Create(Post);
                if (newPost is null) return BadRequest();
                return CreatedAtAction(nameof(Get), new { Id = newPost.Id }, newPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ValidateAndErrorsTools.GetInfo(ex));
            }
        }
        #endregion

        #region Put
        [HttpPut("Update/{id}"), Authorize(Roles = "admin,laborant")]
        public IActionResult Update(int id, [FromBody] Post Post)
        {
            try
            {
                if (id != Post.Id)
                {
                    return BadRequest("Id not matched");
                }
                crudRep.Update(Post);
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
                var Post = crudRep.Read<Post>(id);
                if (Post is null) return NotFound();
                crudRep.Delete(Post);
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
