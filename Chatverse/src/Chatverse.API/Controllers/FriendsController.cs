using Chatverse.Application.Features.Query.AppUser.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> FindFriend()
        {
            GetAllUsersQueryRequest getAllUsersQueryRequest = new GetAllUsersQueryRequest();
            List<GetAllUsersQueryResponse> response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }
    }
}
