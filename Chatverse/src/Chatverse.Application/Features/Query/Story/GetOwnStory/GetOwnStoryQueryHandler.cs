namespace Chatverse.Application.Features.Query.Story.GetOwnStory;

public class GetOwnStoryQueryHandler : IRequestHandler<GetOwnStoryQueryRequest, GetOwnStoryQueryResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly IApplicationDbContext _context;

    public GetOwnStoryQueryHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _context = context;
    }

    public async Task<GetOwnStoryQueryResponse> Handle(GetOwnStoryQueryRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
       var stories =  _context.Stories.Where(x => x.UserId == currentUser.Id);
        GetOwnStoryQueryResponse getOwnStoryQueryResponse = new GetOwnStoryQueryResponse();
        List<OwnStory> ownStories = new List<OwnStory>();
        if(stories is not null)
        {
            foreach(var i in stories)
            {
                OwnStory ownStory = new OwnStory()
                {
                    Media = i.Media,
                    ProfilePicture = currentUser.ProfilePicture
                };
                ownStories.Add(ownStory);
            }
            return new GetOwnStoryQueryResponse()
            {
                Stories = ownStories
            };
        }
        return new GetOwnStoryQueryResponse();
    }
}
