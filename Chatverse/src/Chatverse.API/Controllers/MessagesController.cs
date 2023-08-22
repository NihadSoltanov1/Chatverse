using Chatverse.Application.Features.Query.Message.GetMessageByFriend;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute]string id)
        {
            GetMessageByFriendCommandRequest getMessageByFriendCommandRequest = new GetMessageByFriendCommandRequest();
            getMessageByFriendCommandRequest.FriendId = id;
            var response = await _mediator.Send(getMessageByFriendCommandRequest);
            return Ok(response);
        }



    }
}
