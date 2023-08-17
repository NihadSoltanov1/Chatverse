using Chatverse.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chatverse.Application.Features.Query.Friendship.GetAllFriends
{
    public class GetAllFriendsQueryHandler : IRequestHandler<GetAllFriendsQueryRequest, List<GetAllFriendsQueryResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly IApplicationDbContext _context;

        public GetAllFriendsQueryHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<GetAllFriendsQueryResponse>> Handle(GetAllFriendsQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var friendships = await _context.Friendships.Where(x => x.ReceiverId == currentUser.Id && x.Accept == true).ToListAsync();
            List<GetAllFriendsQueryResponse> responseList = new List<GetAllFriendsQueryResponse>(); 
            if (friendships is not null)
            {
                foreach(var friend in friendships) {
                    var senderUser = await _userManager.FindByIdAsync(friend.SenderId);
                    GetAllFriendsQueryResponse getAllFriendsQueryResponse = new GetAllFriendsQueryResponse()
                    {
                        Fullname = senderUser.FullName,
                        ProfilePicture = senderUser.ProfilePicture,
                        Username = senderUser.UserName,
                        SenderId = senderUser.Id
                    };
                    responseList.Add(getAllFriendsQueryResponse);           
                }
            }
            return responseList;
        }
    }
}
