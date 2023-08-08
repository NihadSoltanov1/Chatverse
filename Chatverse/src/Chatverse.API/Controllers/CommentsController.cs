using Chatverse.Application.Features.Command.Comment.CreateComment;
using Chatverse.Application.Features.Query.Comment.GetCommentByPostId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CommentsController : ControllerBase
    {
        readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentCommandRequest createCommentCommandRequest)
        {
            var response = await _mediator.Send(createCommentCommandRequest);
            return Ok(response.Data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentsByPostId([FromRoute]int id)
        {
            GetCommentByPostIdQueryRequest getCommentByPostIdQueryRequest = new GetCommentByPostIdQueryRequest() { PostId = id };
            GetCommentByPostIdQueryResponse response = await _mediator.Send(getCommentByPostIdQueryRequest);
            return Ok(response.Comments);
        }
    }
}
