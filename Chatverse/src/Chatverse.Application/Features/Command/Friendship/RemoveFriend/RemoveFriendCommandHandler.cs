
using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatverse.Application.Features.Command.Friendship.RemoveFriend
{
    public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommandRequest, IResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public RemoveFriendCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager)
        {
            _currentUserService = currentUserService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(RemoveFriendCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var friendship1 = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == request.Id && x.ReceiverId == currentUser.Id);
            if (friendship1 is not null)
            {
                _context.Friendships.Remove(friendship1);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var friendship2 = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == currentUser.Id && x.ReceiverId == request.Id);
            if(friendship2 is not null)
            {
                _context.Friendships.Remove(friendship2);
                await _context.SaveChangesAsync(cancellationToken);
            }


            return new Result(true, "complate");
        }
    }
}
