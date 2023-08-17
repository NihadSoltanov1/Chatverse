using Chatverse.Application.Features.Command.AppUser.ChangePassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommandRequest changePasswordCommandRequest)
        {
            var response = await _mediator.Send(changePasswordCommandRequest);
            return Ok(response);
        }
    }
}
