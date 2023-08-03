using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using Chatverse.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.CreatePost
{
      public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, IDataResult<CreatePostCommandRequest>>
    {
       private readonly ICurrentUserService _currentUserService;
       private readonly UserManager<Domain.Identity.AppUser> _userManager;
       private readonly IApplicationDbContext _context;
        public CreatePostCommandHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IDataResult<CreatePostCommandRequest>> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            if (currentUser is null) throw new UnauthorizedLoginException("Login to your account to share post");
            Domain.Entities.Post post = new()
            {
                AppUserId = currentUser.Id,
                Content = request.Content,
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync(cancellationToken);
            foreach(string path in request.MediaLocation)
            {
                PostImage postImage = new PostImage
                {
                    PostId = post.Id,
                    FilePath = path
                };
                await _context.PostImages.AddAsync(postImage);
                await _context.SaveChangesAsync(cancellationToken);
            }
                 
            return new SuccessDataResult<CreatePostCommandRequest>(request, "Post added successfully");
        }
    }

}
