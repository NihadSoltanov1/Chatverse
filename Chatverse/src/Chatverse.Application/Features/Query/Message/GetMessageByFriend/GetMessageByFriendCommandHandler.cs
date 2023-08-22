using Chatverse.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatverse.Application.Features.Query.Message.GetMessageByFriend
{
    public class GetMessageByFriendCommandHandler : IRequestHandler<GetMessageByFriendCommandRequest, List<GetMessageByFriendCommandResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public GetMessageByFriendCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager)
        {
            _currentUserService = currentUserService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<GetMessageByFriendCommandResponse>> Handle(GetMessageByFriendCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Identity.AppUser currentUser;
            if(request.CurrentUserId is not null)
            {
                currentUser = await _userManager.FindByIdAsync(request.CurrentUserId);
            }
            else
            {
                currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            }
            var messages = await _context.Messages
                           .Where(m =>
                               (m.SenderId == currentUser.Id && m.ReceiverId == request.FriendId)
                               || (m.SenderId == request.FriendId && m.ReceiverId == currentUser.Id)
                           ).ToListAsync();

            List<GetMessageByFriendCommandResponse> responses = new List<GetMessageByFriendCommandResponse>();

            if(messages is not null)
            {
                foreach (var message in messages)
                {
                    var sender = await _userManager.FindByIdAsync(message.SenderId);
                    var receiver = await _userManager.FindByIdAsync(message.ReceiverId);
                    GetMessageByFriendCommandResponse response = new GetMessageByFriendCommandResponse()
                    {
                        Id = message.Id,
                        Content = message.Content,
                        ReceiverId = receiver.Id,
                        ReceiverUsername = receiver.UserName,
                        ReceiverProfilePicture = receiver.ProfilePicture,
                        SenderId = sender.Id,
                        SenderUsername = sender.UserName,
                        SenderProfilePicture = sender.ProfilePicture,
                        SendDate = message.CreatedDate
                    };
                    responses.Add(response);

                }
            }
            return responses;

        }
    }


}
