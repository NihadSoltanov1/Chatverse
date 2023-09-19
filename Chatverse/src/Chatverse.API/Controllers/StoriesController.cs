using Chatverse.Application.Features.Command.Story.AddStory;
using Chatverse.Application.Features.Query.Story.GetFriendsStory;
using Chatverse.Application.Features.Query.Story.GetOwnStory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class StoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddStory(AddStoryCommandRequest addStoryCommandRequest)
        {
           var response =  await _mediator.Send(addStoryCommandRequest);
           return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetOwnStories()
        {
            GetOwnStoryQueryRequest getOwnStoryQueryRequest = new GetOwnStoryQueryRequest();
            var response = await _mediator.Send(getOwnStoryQueryRequest);
            return Ok(response.Stories);
        }
        [HttpGet]
        public async Task<IActionResult> GetFriendsStories()
        {
            GetFriendsStoryQueryRequest friendStory = new GetFriendsStoryQueryRequest();
            var response = await _mediator.Send(friendStory);
            return Ok(response.Stories);
        }
    }
}
