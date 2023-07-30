using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Comment.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommandRequest, IDataResult<CreateCommentCommandRequest>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommentCommandHandler(IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<IDataResult<CreateCommentCommandRequest>> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);

            if (currentUser is null) throw new UnauthorizedLoginException("Login to your account to write comment");
            Domain.Entities.Comment newComment = new()
            {
                AppUserId = currentUser.Id,
                Content = request.Content,
                PostId = request.PostId
            };
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync(cancellationToken);
            return new SuccessDataResult<CreateCommentCommandRequest>(request, "Comment added successfully");


        }
    }
}
