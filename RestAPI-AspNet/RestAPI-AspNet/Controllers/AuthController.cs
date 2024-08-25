using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.VO;

namespace RestAPI_AspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class AuthController : Controller
    {
        private readonly ILoginBusiness _loginBusiness;

        string msgBadRequest = "Invalid client request";

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("sigin")]
        public IActionResult Sigin([FromBody] UserVO User)
        {
            if (User == null) return BadRequest(msgBadRequest);

            var UserToken = _loginBusiness.ValidateCredentials(User);

            if (UserToken == null) return Unauthorized(msgBadRequest);

            return Ok(UserToken);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null) return BadRequest(msgBadRequest);

            var token = _loginBusiness.ValidateCredentials(tokenVO);

            if (token == null) return BadRequest(msgBadRequest);

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);

            if (!result) return BadRequest(msgBadRequest);
            return NoContent();

        }


    }
}
