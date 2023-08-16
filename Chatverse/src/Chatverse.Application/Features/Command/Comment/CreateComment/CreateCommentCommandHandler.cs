using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using Chatverse.Application.Features.Command.Notification.CreateNotification;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMediator _mediator;
        public CreateCommentCommandHandler(IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService, IMediator mediator)
        {
            _context = context;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _mediator = mediator;
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

            var posts = await _context.Posts.FirstOrDefaultAsync(p => p.Id == newComment.PostId);
            
            if(posts.AppUserId != newComment.AppUserId)
            {
                CreateNotificationCommandRequest createNotificationCommandRequest = new CreateNotificationCommandRequest();
                createNotificationCommandRequest.CommentId = newComment.Id;
                createNotificationCommandRequest.PostId = newComment.PostId;
                string firstHalf = "";
                int halfLength = newComment.Content.Length / 2;
                for (int i = 0; i < halfLength; i++)
                {
                    firstHalf += newComment.Content[i];
                }
                createNotificationCommandRequest.CommentContent = firstHalf.Trim();
                createNotificationCommandRequest.CurrentUserId = posts.AppUserId;
                createNotificationCommandRequest.CategoryName = "WC";
                await _mediator.Send(createNotificationCommandRequest);
            }
            

            return new SuccessDataResult<CreateCommentCommandRequest>(request, "Comment added successfully");


        }
    }
}
