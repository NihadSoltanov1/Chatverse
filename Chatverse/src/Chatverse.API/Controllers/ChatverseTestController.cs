using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatverseTestController : ControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetName()
        {
            string myname = "Nihad";
            return Ok(myname);
        }
    }
}
