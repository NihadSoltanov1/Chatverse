using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Interfaces.MongoDb;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Message.CreateMessage
{

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommandRequest, SendMessageCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMongoCollection<Domain.Entities.Message> _messageColection;
        private readonly ICurrentUserService _currentUser;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public SendMessageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, UserManager<Domain.Identity.AppUser> userManager,IDatabaseSettings databaseSettings)
        {
            _context = context;
            _currentUser = currentUser;
            _userManager = userManager;

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            this._messageColection = database.GetCollection<Domain.Entities.Message>(databaseSettings.MessageCollectionName);


        }

        public async Task<SendMessageCommandResponse> Handle(SendMessageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Identity.AppUser currentUser;
            if (request.FromUserId is not null) currentUser = await _userManager.FindByIdAsync(request.FromUserId);
            else currentUser = await _userManager.FindByNameAsync(_currentUser.UserName);
            var receiverUser = await _userManager.FindByIdAsync(request.ToUserId);




            Domain.Entities.Message newMessage = new Domain.Entities.Message()
            {
                Content = request.Content,
                ReceiverId = request.ToUserId,
                SenderId = currentUser.Id,
                SendDate = DateTime.UtcNow,
                Image = request.Image
            };



            await _messageColection.InsertOneAsync(newMessage);

            return new SendMessageCommandResponse()
            {
                MessageId = newMessage.MessageId.ToString(),
                Content = request.Content,
                SenderId = currentUser.Id,
                SenderUsername = currentUser.UserName,
                SenderProfilePicture = currentUser.ProfilePicture,
                ReceiverId = receiverUser.Id,
                ReceiverProfilePicture = receiverUser.ProfilePicture,
                ReceiverUsername = receiverUser.UserName,
                SendDate=  newMessage.SendDate,
                Image = newMessage.Image
            };


        }
    }
}
