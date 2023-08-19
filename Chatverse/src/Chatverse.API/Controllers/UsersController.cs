using Chatverse.Application.Features.Command.AppUser.UpdateInformation;
using Chatverse.Application.Features.Query.AppUser.GetAuthorUserShortInformation;
using Chatverse.Application.Features.Query.AppUser.GetUserInformation;
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
        [HttpGet]
        public async Task<IActionResult> ShortInformation()
        {
            GetAuthorUserShortInformationQueryRequest getAuthorUserShortInformationQueryRequest = new GetAuthorUserShortInformationQueryRequest();
            GetAuthorUserShortInformationQueryResponse response = await _mediator.Send(getAuthorUserShortInformationQueryRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateInformation(UpdateInformationCommandRequest updateInformationCommandRequest)
        {
            var response = await _mediator.Send(updateInformationCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountInformation()
        {
            GetUserInformationCommandRequest getUserInformationCommandRequest = new GetUserInformationCommandRequest();
            var response = await _mediator.Send(getUserInformationCommandRequest);
            return Ok(response);
        }


    }
}
