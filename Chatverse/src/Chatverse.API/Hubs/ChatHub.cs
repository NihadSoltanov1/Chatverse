
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;

namespace Chatverse.UI.Hubs
{
    public class ChatHub : Hub
    {
       

        public override async Task OnConnectedAsync()
        {               
            await base.OnConnectedAsync();
        }
    }
}
