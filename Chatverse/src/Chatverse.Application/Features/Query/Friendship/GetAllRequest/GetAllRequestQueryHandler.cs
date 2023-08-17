using Chatverse.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chatverse.Application.Features.Query.Friendship.GetAllRequest
{
    public class GetAllRequestQueryHandler : IRequestHandler<GetAllRequestQueryRequest, List<GetAllRequestQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        public GetAllRequestQueryHandler(IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService)
        {
            _context = context;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<List<GetAllRequestQueryResponse>> Handle(GetAllRequestQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var friendShips = _context.Friendships.Where(f => f.ReceiverId == currentUser.Id && f.Accept==false).ToList();
            List<GetAllRequestQueryResponse> requests = new List<GetAllRequestQueryResponse>();

            if(friendShips is not null)
            {
                var latestRequest = friendShips.OrderByDescending(n => n.CreatedDate).ToList();
                foreach (var friendship in latestRequest) {
                    var senderUser = await _userManager.FindByIdAsync(friendship.SenderId);
                    GetAllRequestQueryResponse getAllRequestQueryResponse = new GetAllRequestQueryResponse()
                    {
                        Fullname = senderUser.FullName,
                        ProfilePicture = senderUser.ProfilePicture,
                        Id = friendship.Id,
                        Username = senderUser.UserName
                    };
                    requests.Add(getAllRequestQueryResponse);
                }
                return requests;
            }

            return requests;
        }
    }
}
