using Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HubConnectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HubConnectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateHub(CreateHubConnectionCommandRequest createHubConnectionCommandRequest)
        {
          var response =   await _mediator.Send(createHubConnectionCommandRequest);
            return Ok(response);
        }
    }
}
