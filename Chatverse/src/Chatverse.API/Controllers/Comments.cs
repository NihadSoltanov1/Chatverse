using Chatverse.Application.Features.Query.Comment.GetCommentByPostId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Comments : ControllerBase
    {
        readonly IMediator _mediator;

        public Comments(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByPostId(int id)
        {
            GetCommentByPostIdQueryRequest getCommentByPostIdQueryRequest = new GetCommentByPostIdQueryRequest() { PostId = id };
            GetCommentByPostIdQueryResponse response = await _mediator.Send(getCommentByPostIdQueryRequest);
            return Ok(response.Comments);
        }
    }
}
