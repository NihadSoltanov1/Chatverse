using Chatverse.Application.Common.Hubs;
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
        public NotificationHubService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendNotificationToClient(string message, string username, string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(ReceiveFunctionNames.NotificationAddedMessage, message, username);            
        }

       




    }
}
