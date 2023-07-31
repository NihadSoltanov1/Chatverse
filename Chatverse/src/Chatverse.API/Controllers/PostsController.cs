using Chatverse.Application.Features.Command.Post.CreatePost;
using Chatverse.Application.Features.Query.Post.GetPostByFriend;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostsController : ControllerBase
    {
        IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostCommandRequest createPostCommandRequest)
        {
            var response = await _mediator.Send(createPostCommandRequest);
            return Ok(response.Message);
        }
        [HttpGet]
        public async Task<IActionResult> GetPostsByFriend()
        {
            GetPostByFriendQueryRequest request = new GetPostByFriendQueryRequest();
           GetPostByFriendQueryResponse response =  await  _mediator.Send(request);
            return Ok(response.Posts);
        }
    }
}
