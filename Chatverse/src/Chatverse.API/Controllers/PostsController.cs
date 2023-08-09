using Chatverse.Application.Features.Command.Post.CreatePost;
using Chatverse.Application.Features.Command.Post.DeletePost;
using Chatverse.Application.Features.Query.Post.GetPostByFriend;
using Chatverse.Application.Features.Query.Post.GetPostById;
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
            GetPostByFriendQueryResponse response = await _mediator.Send(request);
            return Ok(response.Posts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            DeletePostCommandRequest deletePostCommandRequest = new DeletePostCommandRequest();
            deletePostCommandRequest.Id = id;
            var response = await _mediator.Send(deletePostCommandRequest);
            return Ok(response.Data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            GetPostByIdQueryRequest getPostByIdQueryRequest = new GetPostByIdQueryRequest();
            getPostByIdQueryRequest.Id = id;
            GetPostByIdQueryResponse response = await _mediator.Send(getPostByIdQueryRequest);
            return Ok(response);
        }
    }
}
