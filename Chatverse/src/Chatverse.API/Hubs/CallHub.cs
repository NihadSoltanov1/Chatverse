using Microsoft.AspNetCore.SignalR;

namespace Chatverse.API.Hubs
{
    public class CallHub : Hub
    {
        public async Task JoinRoom(string roomId, string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("user-connected", userId);
        }



    }
}

