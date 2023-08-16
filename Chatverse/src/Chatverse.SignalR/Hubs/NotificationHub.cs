using Chatverse.Application.Features.Command.Comment.CreateComment;
using Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;
using Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection;
using Chatverse.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IMediator _mediator;

        public NotificationHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }
        public override  Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            DeleteHubConnectionCommandRequest deleteHubConnectionCommandRequest = new DeleteHubConnectionCommandRequest();
            deleteHubConnectionCommandRequest.ConnecitionId = connectionId;
             _mediator.Send(deleteHubConnectionCommandRequest);

            return base.OnDisconnectedAsync(exception);
        }
        public async Task SaveUserConnection()
        {
            var connectionId = Context.ConnectionId;
            CreateHubConnectionCommandRequest createHubConnectionCommandRequest = new CreateHubConnectionCommandRequest();
            createHubConnectionCommandRequest.ConnectionId = connectionId;
            await _mediator.Send(createHubConnectionCommandRequest);
        }
    }
}
