using Chatverse.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chatverse.ChatPage.Hubs
{
    public class ChatHub : Hub
    {
      

    

        public async Task SendMessage(string message,string username)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
