using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOCIS_API.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRep _AuthRep;
        private readonly IConfiguration _Configuration;

        public AuthController(IAuthRep authRep, IConfiguration configuration)
        {
            _AuthRep = authRep;
            _Configuration = configuration;
        }

        [HttpPost("SetNewPassword"),Authorize(Roles ="admin")]
        public IActionResult SetNewPassword(string username,string password)
        {
             bool result = _AuthRep.SetNewPassword(username,password);
            if (result) return Ok();
            else return BadRequest();
         }
        [HttpPost("Authorize")]
        public IActionResult Authorize(string username,string password){
            Person? person = _AuthRep.ValidPerson(username, password);
            if (person is not null)
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, person.UserName),
                    new Claim(ClaimTypes.Role, person.Role.Role1),
                    new Claim("Id",person.Id.ToString())
                });
                return Ok(new { Token = JwtTools.GenerateJwtToken(identity, _Configuration["AuthOptions:key"], _Configuration["AuthOptions:Issuer"], _Configuration["AuthOptions:Audience"])});
            }
            return Unauthorized();
        }
    }
}
