using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Like.UnlikePost
{
    public class UnlikePostCommandRequest : IRequest<IResult>
    {
        public int Id { get; set; }
    }

    public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommandRequest, IResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public UnlikePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(UnlikePostCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var postLike = _context.Likes.FirstOrDefault(l => l.PostId == request.Id && l.AppUserId == currentUser.Id);
            _context.Likes.Remove(postLike);
            await _context.SaveChangesAsync(cancellationToken);
            return new Result(true, "Complated");
        }
    }
}
