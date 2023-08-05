using Chatverse.Application.Features.Query.Post.GetPostByAuthorUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthorPosts()
        {
            GetPostByAuthorUserIdQueryRequest getPostByAuthorUserIdQueryRequest = new GetPostByAuthorUserIdQueryRequest();
            GetPostByAuthorUserIdQueryResponse response = await _mediator.Send(getPostByAuthorUserIdQueryRequest);
            return Ok(response.Posts);
        }
    }
}
