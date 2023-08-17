using Chatverse.Application.DTOs.SingleDto;
using Chatverse.Application.Features.Command.Friendship.AcceptFriendRequest;
using Chatverse.Application.Features.Command.Friendship.CreateFriendship;
using Chatverse.Application.Features.Command.Friendship.DeleteFriendshipRequest;
using Chatverse.Application.Features.Command.Friendship.RemoveFriend;
using Chatverse.Application.Features.Query.Friendship.GetAllFriends;
using Chatverse.Application.Features.Query.Friendship.GetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class FriendshipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendshipsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddFriend(IdDto idDto)
        {
            CreateFriendshipCommandRequest createFriendshipCommandRequest = new CreateFriendshipCommandRequest();
            createFriendshipCommandRequest.ReceiverId = idDto.Id;
            var response = await _mediator.Send(createFriendshipCommandRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFriendRequest()
        {
            GetAllRequestQueryRequest getAllRequestQueryRequest = new GetAllRequestQueryRequest();
            var response = await _mediator.Send(getAllRequestQueryRequest);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFriend([FromRoute] string id)
        {
            RemoveFriendCommandRequest removeFriendCommandRequest = new RemoveFriendCommandRequest();
            removeFriendCommandRequest.Id = id;
            var response  =  await _mediator.Send(removeFriendCommandRequest);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFriends()
        {
            GetAllFriendsQueryRequest getAllFriendsQueryRequest = new GetAllFriendsQueryRequest();
            var response = await _mediator.Send(getAllFriendsQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(CreateFriendshipCommandRequest createFriendshipCommandRequest)
        {
            await _mediator.Send(createFriendshipCommandRequest);
            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AcceptFriendRequest([FromRoute]int id)
        {
            AcceptFriendRequestCommandRequest acceptFriendRequestCommandRequest = new AcceptFriendRequestCommandRequest();
            acceptFriendRequestCommandRequest.FrienshipId = id;
            var response = await _mediator.Send(acceptFriendRequestCommandRequest);
            return Ok(response);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendRequest([FromRoute]int id)
        {
            DeleteFriendshipRequestCommandRequest deleteFriendshipRequestCommandRequest = new DeleteFriendshipRequestCommandRequest();
            deleteFriendshipRequestCommandRequest.FriendshipId = id;
           var response =  await _mediator.Send(deleteFriendshipRequestCommandRequest);
            return Ok(response.Message);
        }




    }
}
