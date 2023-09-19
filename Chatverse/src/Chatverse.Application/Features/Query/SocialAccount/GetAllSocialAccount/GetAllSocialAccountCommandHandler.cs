namespace Chatverse.Application.Features.Query.SocialAccount.GetAllSocialAccount;

public class GetAllSocialAccountCommandHandler : IRequestHandler<GetAllSocialAccountCommandRequest, List<GetAllSocialAccountCommandResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;

    public GetAllSocialAccountCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<List<GetAllSocialAccountCommandResponse>> Handle(GetAllSocialAccountCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        var socialAccounts = _context.SocialAccounts.Where(x => x.UserId == currentUser.Id).ToList();
        List<GetAllSocialAccountCommandResponse> responses = new();
        if (socialAccounts is not null)
        { 
            foreach(var social in socialAccounts)
            {
                GetAllSocialAccountCommandResponse response = new GetAllSocialAccountCommandResponse()
                {
                    Category = social.Category,
                    Url = social.Link
                };
                responses.Add(response);
            }
        }
        return responses;
    }
}
