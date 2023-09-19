namespace Chatverse.Application.Features.Query.Story.GetFriendsStory;

public class GetFriendsStoryQueryRequest : IRequest<GetFriendsStoryQueryResponse>
{

}
public class GetFriendsStoryQueryHandler : IRequestHandler<GetFriendsStoryQueryRequest, GetFriendsStoryQueryResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly IApplicationDbContext _context;

    public GetFriendsStoryQueryHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _context = context;
    }
    public async Task<GetFriendsStoryQueryResponse> Handle(GetFriendsStoryQueryRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        List<Domain.Entities.Friendship> friendship = await _context.Friendships.Where(x => x.SenderId == currentUser.Id).ToListAsync();
        if (friendship is not null)
        {
            List<FriendsStory> storyList = new List<FriendsStory>();
            foreach (var friend in friendship)
            {
                if(friend.Accept == true)
                {
                    var friendUser = await _userManager.FindByIdAsync(friend.ReceiverId);
                  
                        List<Domain.Entities.Story> getStoryByFriend = await _context.Stories.Where(x => x.UserId == friend.ReceiverId).ToListAsync();
                        if (getStoryByFriend is not null)
                        {
                            foreach (var i in getStoryByFriend)
                            {
                            FriendsStory friendsStory = new FriendsStory()
                            {
                                FriendId = friendUser.Id,
                                Media = i.Media,
                                ProfilePicture = friendUser.ProfilePicture,
                                Username = friendUser.UserName
                            };
                            storyList.Add(friendsStory);
                        }
                          
                       } 
                }
            }

            return new GetFriendsStoryQueryResponse()
            {
                Stories = storyList
            };


        }
        return new GetFriendsStoryQueryResponse();
    }
}
