using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Exceptions;
using Chatverse.Application.Features.Query.Comment.GetCommentByPostId;
using Chatverse.Application.Features.Query.Post.GetPostByFriend;
using Chatverse.Domain.Entities;
using Chatverse.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Post.GetPostByAuthorUserId
{
    public class GetPostByAuthorUserIdQueryHandler : IRequestHandler<GetPostByAuthorUserIdQueryRequest, GetPostByAuthorUserIdQueryResponse>
    {
        readonly ICurrentUserService _currentUser;
        readonly UserManager<AppUser> _userManager;
        readonly IApplicationDbContext _context;
        readonly IMediator _mediator;
        public GetPostByAuthorUserIdQueryHandler(ICurrentUserService currentUser, UserManager<AppUser> userManager, IApplicationDbContext context, IMediator mediator)
        {
            _currentUser = currentUser;
            _userManager = userManager;
            _context = context;
            _mediator = mediator;
        }

        public async Task<GetPostByAuthorUserIdQueryResponse> Handle(GetPostByAuthorUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUser.UserName);
            if (currentUser is null) throw new NotFoundException();
            var posts = await _context.Posts.Where(p => p.AppUserId == currentUser.Id).ToListAsync();
            if (!posts.Any()) throw new NotFoundAnyPostException("You haven't shared any posts yet");
            List<GetMyPosts> getMyPosts = new List<GetMyPosts>();

            posts.ForEach(async post =>
            {
                var comment = await _mediator.Send(new GetCommentByPostIdQueryRequest() { PostId = post.Id });

                var getPosts = new GetMyPosts()
                {
                    PostId = post.Id,
                    Content = post.Content,
                    FullName = currentUser.FullName,
                    Media = _context.PostImages.Where(p => p.PostId == post.Id)
                        .Select(i => i.FilePath).ToList(),
                    CreateDate = post.CreatedDate,
                    Comments = comment.Comments

                };
                getMyPosts.Add(getPosts);
            });

            return new GetPostByAuthorUserIdQueryResponse()
            {
                Posts = getMyPosts
            };
        }
    }
}
