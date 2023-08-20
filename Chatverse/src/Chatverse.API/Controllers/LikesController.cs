using Chatverse.Application.Features.Command.Like.LikePost;
using Chatverse.Application.Features.Command.Like.UnlikePost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class LikesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LikesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> LikePost([FromRoute]int id)
        {
            LikePostCommandRequest likePostCommandRequest = new LikePostCommandRequest();
            likePostCommandRequest.PostId = id;
            var reponse = await _mediator.Send(likePostCommandRequest);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UnlikePost([FromRoute] int id)
        {
            UnlikePostCommandRequest unlikePostCommandRequest = new UnlikePostCommandRequest();
            unlikePostCommandRequest.Id = id;
            var response = await _mediator.Send(unlikePostCommandRequest);
            return Ok(response);
        }


    }
}
