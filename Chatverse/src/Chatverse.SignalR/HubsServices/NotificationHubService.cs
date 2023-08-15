using Chatverse.Application.Common.Hubs;
using Chatverse.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
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

        public async Task NotificationAddedMessageAsync(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.NotificationAddedMessage, message);
        }
    }
}
4169738559715788