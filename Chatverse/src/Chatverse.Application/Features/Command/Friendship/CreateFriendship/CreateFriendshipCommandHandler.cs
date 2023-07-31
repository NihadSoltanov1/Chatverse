using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Friendship.CreateFriendship
{
    public class CreateFriendshipCommandHandler : IRequestHandler<CreateFriendshipCommandRequest, IDataResult<CreateFriendshipCommandRequest>>
    {
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;

        public CreateFriendshipCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<IDataResult<CreateFriendshipCommandRequest>> Handle(CreateFriendshipCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var currentUserId = currentUser.Id;
            var receiverUser = await _userManager.FindByIdAsync(request.ReceiverId);
            if (receiverUser is null) throw new NotFoundException("This user can not found");
            var isFriend = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == currentUserId && x.ReceiverId == request.ReceiverId && x.State == true);

            if (isFriend is not null)
            {
                if (isFriend.Accept == false) throw new WaitingAcceptToFriendRequestException();
                throw new AlreadyFriendEachOtherException();
            }
            var _isFriend = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == request.ReceiverId && x.ReceiverId == currentUserId && x.State == true);
            if (_isFriend is not null)
            {
                if (_isFriend.Accept == false) throw new WaitingAcceptToFriendRequestException();
                throw new AlreadyFriendEachOtherException();
            }

            Domain.Entities.Friendship newFriendship = new()
            {
                SenderId = currentUserId,
                ReceiverId = receiverUser.Id
            };
            await _context.Friendships.AddAsync(newFriendship);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessDataResult<CreateFriendshipCommandRequest>(request, "Send friend request successfully");


        }
    }

}
