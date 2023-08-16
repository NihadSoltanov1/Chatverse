using Chatverse.Application.Common.Hubs;
using Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;
using Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection;
using Chatverse.Domain.Entities;
using Chatverse.SignalR.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.SignalR.HubsServices
{
    public class NotificationHubService : INotificationHubService
    {
        readonly IHubContext<NotificationHub> _hubContext;
        readonly IMediator _mediator;
        public NotificationHubService(IHubContext<NotificationHub> hubContext, IMediator mediator)
        {
            _hubContext = hubContext;
            _mediator = mediator;
        }
        public async Task SendNotificationToClient(string message, string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(ReceiveFunctionNames.NotificationAddedMessage, message);            
        }

      

        public async Task SendDisConnectedMessage(string connectionId)
        {
           
            DeleteHubConnectionCommandRequest deleteHubConnectionCommandRequest = new DeleteHubConnectionCommandRequest();
            deleteHubConnectionCommandRequest.ConnecitionId = connectionId;
           await _mediator.Send(deleteHubConnectionCommandRequest);
        }

        public async Task SaveUserToHub(string connectionId)
        {       
            CreateHubConnectionCommandRequest createHubConnectionCommandRequest = new CreateHubConnectionCommandRequest();
            createHubConnectionCommandRequest.ConnectionId = connectionId;
            await _mediator.Send(createHubConnectionCommandRequest);
        }


    }
}


