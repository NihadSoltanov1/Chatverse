using Chatverse.Application.Common.Interfaces;
using Chatverse.Domain.Entities;
using Chatverse.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Post.GetPostByFriend
{
    public class GetPostByFriendQueryHandler : IRequestHandler<GetPostByFriendQueryRequest, GetPostByFriendQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        public GetPostByFriendQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<GetPostByFriendQueryResponse> Handle(GetPostByFriendQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            Domain.Entities.Friendship friendship = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == currentUser.Id);
            if (friendship is null) throw new Exception();
            if (friendship.Accept == true)
            {
                List<Domain.Entities.Post> getPostByFriend = await _context.Posts.Where(x => x.AppUserId == friendship.ReceiverId).ToListAsync();
                if (getPostByFriend is null) throw new Exception();
                List<string> postImage = new List<string>();
                string path = "";
               
                List<GetFriendsPosts> postsList = new List<GetFriendsPosts>();

                foreach (var post in getPostByFriend)
                {
                    var sharePostUser = await _userManager.FindByIdAsync(post.AppUserId);
                    var friendsPost = new GetFriendsPosts
                    {
                        FullName = sharePostUser.FullName,
                        Content = post.Content,
                        Media = _context.PostImages.Where(p => p.PostId == post.Id)
                        .Select(i=>i.FilePath).ToList(),
                        CreateDate = post.CreatedDate
                    };

                    postsList.Add(friendsPost);
                }

                return new GetPostByFriendQueryResponse
                {
                    Posts = postsList
                };
            }
            return new GetPostByFriendQueryResponse();


        }
    }
}
