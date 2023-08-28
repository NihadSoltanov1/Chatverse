
using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;
using Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection;
using Chatverse.Application.Features.Command.Message.CreateMessage;
using Chatverse.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Text;

namespace Chatverse.UI.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        public ChatHub(IMediator mediator, IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
        }





        public async Task SendMessageAsync(string toUser, string? content, string? imagePath) 
        {
            SendMessageCommandRequest sendMessageCommandRequest = new SendMessageCommandRequest();
            sendMessageCommandRequest.Content = content;
            sendMessageCommandRequest.FromUserId = Context.UserIdentifier;
            sendMessageCommandRequest.ToUserId = toUser;
            sendMessageCommandRequest.Image = imagePath;
            SendMessageCommandResponse response = await _mediator.Send(sendMessageCommandRequest);

            string hour = response.SendDate.ToString("h:mm tt").ToLower();


            var hubConnection1 = await _context.HubConnections.FirstOrDefaultAsync(x => x.Username == response.ReceiverUsername);




            if(hubConnection1 is not null)
            {
                await Clients.Client(hubConnection1.ConnectionId).SendAsync("seeSendMessage", response.SenderUsername, response.SenderProfilePicture, hour, content, imagePath);
            }
           
            await Clients.Client(Context.ConnectionId).SendAsync("seeMySendMessage", response.SenderUsername,response.SenderProfilePicture,content, hour, imagePath);
        }



        public async Task Typing(string receiverId, bool isTyping)
        {
           var user = await _userManager.FindByIdAsync(receiverId);
            var currentUser = await _userManager.FindByIdAsync(Context.UserIdentifier);
            if (user is not null)
            {
                var hubConnection2 = await _context.HubConnections.FirstOrDefaultAsync(x => x.Username == user.UserName);
                if(hubConnection2 is not null)
                {
                  var connectionId2 = hubConnection2.ConnectionId;
                    await Clients.Client(connectionId2).SendAsync("showtousertyping", currentUser.ProfilePicture, currentUser.UserName,isTyping);
                }
            }
        }






        public override async Task OnConnectedAsync()
        {
           

            CreateHubConnectionCommandRequest createHubConnectionCommandRequest = new CreateHubConnectionCommandRequest();
            createHubConnectionCommandRequest.ConnectionId = Context.ConnectionId;
            createHubConnectionCommandRequest.UserId = Context.UserIdentifier;
            await _mediator.Send(createHubConnectionCommandRequest);

            var currentUser = await _userManager.FindByIdAsync(Context.UserIdentifier);
            var getFriends1 = await _context.Friendships.Where(x => x.SenderId == Context.UserIdentifier && x.Accept == true).ToListAsync();
            if (getFriends1.Count != 0)
            {
                foreach(var friend in getFriends1)
                {
                    var friend1 = await _userManager.FindByIdAsync(friend.ReceiverId);
                    var hubConnection1 = await _context.HubConnections.FirstOrDefaultAsync(x => x.Username == friend1.UserName);
                    if(hubConnection1 is not null)
                    {
                       await Clients.Client(hubConnection1.ConnectionId).SendAsync("seeOnlineFriend", currentUser.UserName,currentUser.ProfilePicture,Context.ConnectionId);
                    await Clients.Client(Context.ConnectionId).SendAsync("seeMyOnlineFriend", friend1.UserName, friend1.ProfilePicture, hubConnection1.ConnectionId);
                    }
                   
                }
                
            }


           

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            DeleteHubConnectionCommandRequest deleteHubConnectionCommand = new DeleteHubConnectionCommandRequest();
            deleteHubConnectionCommand.ConnecitionId = Context.ConnectionId;
            await _mediator.Send(deleteHubConnectionCommand);





            var currentUser = await _userManager.FindByIdAsync(Context.UserIdentifier);
            var getFriends1 = await _context.Friendships.Where(x => x.SenderId == Context.UserIdentifier && x.Accept == true).ToListAsync();
            if (getFriends1.Count != 0)
            {
                foreach (var friend in getFriends1)
                {
                    var friend1 = await _userManager.FindByIdAsync(friend.ReceiverId);
                    var hubConnection1 = await _context.HubConnections.FirstOrDefaultAsync(x => x.Username == friend1.UserName);
                    if (hubConnection1 is not null)
                    {
                        await Clients.Client(hubConnection1.ConnectionId).SendAsync("deleteOnlineUser", Context.ConnectionId);
                    }
                }

            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
