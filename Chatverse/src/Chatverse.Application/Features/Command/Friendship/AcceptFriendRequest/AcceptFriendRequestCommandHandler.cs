using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatverse.Application.Features.Command.Friendship.AcceptFriendRequest
{
    public class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommandRequest, IResult>
    {
        private readonly ICurrentUserService _curreUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly IApplicationDbContext _context;

        public AcceptFriendRequestCommandHandler(ICurrentUserService curreUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context)
        {
            _curreUserService = curreUserService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IResult> Handle(AcceptFriendRequestCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_curreUserService.UserName);
            var friendShip = await _context.Friendships.FirstOrDefaultAsync(f => f.Id == request.FrienshipId);
            if(friendShip is not null)
            {
                friendShip.Accept = true;
                _context.Friendships.Update(friendShip);
                await _context.SaveChangesAsync(cancellationToken);

                Domain.Entities.Friendship acceptedFriendship = new Domain.Entities.Friendship()
                {
                    Accept = true,
                    ReceiverId = friendShip.SenderId,
                    SenderId = currentUser.Id
                };
                await _context.Friendships.AddAsync(acceptedFriendship);
                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, "Friend request accepted successfully");
            }
            return new Result(false, "Failed");

        }
    }
}
