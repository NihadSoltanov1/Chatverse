using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Like.LikePost
{
    public class LikePostCommandRequest : IRequest<IResult>
    {
        public int PostId { get; set; }
    }
    public class LikePostCommandHandler : IRequestHandler<LikePostCommandRequest, IResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public LikePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(LikePostCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            Domain.Entities.Like newLike = new Domain.Entities.Like()
            {
                AppUserId = currentUser.Id,
                PostId = request.PostId
            };
            await _context.Likes.AddAsync(newLike);
            await _context.SaveChangesAsync(cancellationToken);
            return new Result(true, "complated");
        }
    }
}
