using Chatverse.Application.Features.Query.Comment.GetCommentByPostId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
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
