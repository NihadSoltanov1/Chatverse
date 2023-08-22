using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        private readonly ICurrentUserService _currentUser;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public SendMessageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUser = currentUser;
            _userManager = userManager;
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
                SenderId = currentUser.Id
            };
            await _context.Messages.AddAsync(newMessage);
            await _context.SaveChangesAsync(cancellationToken);

            return new SendMessageCommandResponse()
            {
                MessageId = newMessage.Id,
                Content = request.Content,
                SenderId = currentUser.Id,
                SenderUsername = currentUser.UserName,
                SenderProfilePicture = currentUser.ProfilePicture,
                ReceiverId = receiverUser.Id,
                ReceiverProfilePicture = receiverUser.ProfilePicture,
                ReceiverUsername = receiverUser.UserName,
                SendDate=  newMessage.CreatedDate
            };


        }
    }
}
