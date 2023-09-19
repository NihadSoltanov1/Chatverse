namespace Chatverse.Application.Features.Query.AppUser.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, List<GetAllUsersQueryResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;

    public GetAllUsersQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        var allUsers = await _userManager.Users.ToListAsync();
        List<GetAllUsersQueryResponse> responseUsers = new List<GetAllUsersQueryResponse>();

        allUsers.ForEach(user =>
        {
            var friendShips = _context.Friendships.Where(f => (f.SenderId == currentUser.Id && f.ReceiverId == user.Id) || (f.SenderId == user.Id && f.ReceiverId == currentUser.Id)).FirstOrDefault();

            if(friendShips is null && user.EmailConfirmed == true && currentUser.Id!=user.Id)
            {
                GetAllUsersQueryResponse responseUser = new GetAllUsersQueryResponse()
                {
                    UserId = user.Id,
                    BackgroundPicture = user.BackgroundPicture,
                    ProfilePicture = user.ProfilePicture,
                    Fullname = user.FullName,
                    Username = user.UserName
                };
                responseUsers.Add(responseUser);
            }
        });
        return responseUsers;


      
       







    }
}
