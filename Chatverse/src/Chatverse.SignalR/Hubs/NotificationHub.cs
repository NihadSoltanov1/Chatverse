using Chatverse.Application.Common.Hubs;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Chatverse.SignalR.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly INotificationHubService _notificationHubService;

        public NotificationHub(INotificationHubService notificationHubService)
        {
            _notificationHubService = notificationHubService;
        }

        public override async Task OnConnectedAsync()
        {

           
            await base.OnConnectedAsync();
            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected");
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
            await _notificationHubService.SendDisConnectedMessage(Context.ConnectionId);
        }
        public async Task SaveUserConnection()
        {
           string connectionId = Context.ConnectionId;
           await _notificationHubService.SaveUserToHub(connectionId);
        }
    }
}
