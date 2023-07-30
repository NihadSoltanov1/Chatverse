using Chatverse.Application.Features.Command.AppUser.Login;
using Chatverse.Application.Features.Command.AppUser.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterCommandRequest userRegisterCommandRequest)
        {
            await _mediator.Send(userRegisterCommandRequest);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse loginUser = await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUser.Token.AccessToken);
        }
    }
}
