using Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Chatverse.Chat.UI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddToHubConnection()
        {
            CreateHubConnectionCommandRequest createHubConnectionCommandRequest = new CreateHubConnectionCommandRequest();
            createHubConnectionCommandRequest.ConnectionId = Context.ConnectionId;
           var response = await _mediator.Send(createHubConnectionCommandRequest);


            await Clients.Others.SendAsync("clientJoined", response.Username);


        }
    }
}
