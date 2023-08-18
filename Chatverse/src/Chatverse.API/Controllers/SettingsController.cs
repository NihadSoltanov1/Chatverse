using Chatverse.Application.Features.Command.AppUser.ChangePassword;
using Chatverse.Application.Features.Command.SocialAccount.CreateSocialAccount;
using Chatverse.Application.Features.Query.SocialAccount.GetAllSocialAccount;
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

        [HttpGet]
        public async Task<IActionResult> GetAllSocialMedia()
        {
            GetAllSocialAccountCommandRequest getAllSocialAccountCommandRequest = new GetAllSocialAccountCommandRequest();
            var response = await _mediator.Send(getAllSocialAccountCommandRequest);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(List<SocialMedia> socialMedias)
        {
            CreateSocialAccountCommandRequest createSocialAccountCommandRequest = new CreateSocialAccountCommandRequest();
            createSocialAccountCommandRequest.SocialMedias = socialMedias;
            var response = await _mediator.Send(createSocialAccountCommandRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommandRequest changePasswordCommandRequest)
        {
            var response = await _mediator.Send(changePasswordCommandRequest);
            return Ok(response);
        }
    }
}
