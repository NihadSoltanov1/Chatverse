using Chatverse.Application.Features.Command.SocialAccount.CreateSocialAccount;
using Chatverse.Application.Features.Query.Notification.GetUserNotification;
using Chatverse.Application.Features.Query.Notification.GetUserNotificationQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class NotificationsController : ControllerBase
    {
        readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserNotification()
        {
            GetUserNotificationQueryRequest getUserNotificationQueryRequest = new GetUserNotificationQueryRequest();
            List<GetUserNotificationQueryResponse> response = await _mediator.Send(getUserNotificationQueryRequest);
            return Ok(response);
        }


       


    }
}
