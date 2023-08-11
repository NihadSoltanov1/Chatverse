using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.DTOs.EmailDto;
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
        IEmailService _email;
        public AuthController(IMediator mediator, IEmailService email)
        {
            _mediator = mediator;
            _email = email;
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

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmDto confirmDto)
        {
            if (string.IsNullOrWhiteSpace(confirmDto.userId) || string.IsNullOrWhiteSpace(confirmDto.token)) return NotFound();
            var result = await _email.ConfirmEmail(confirmDto.userId, confirmDto.token);
            return Ok(result.Success);
        }
    }
}
