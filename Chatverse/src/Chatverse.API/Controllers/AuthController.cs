using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.DTOs.EmailDto;
using Chatverse.Application.Features.Command.AppUser.ForgetPassword;
using Chatverse.Application.Features.Command.AppUser.ForgetPassword.UpdatePassword;
using Chatverse.Application.Features.Command.AppUser.Login;
using Chatverse.Application.Features.Command.AppUser.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            if (!ModelState.IsValid)
            {
                 
            }
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
        public async Task<IActionResult> ResetPassword(ForgetPasswordCommandRequest Email)
        {

            var response = await _mediator.Send(Email);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> SetNewPassword(UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            var result = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(result);
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
