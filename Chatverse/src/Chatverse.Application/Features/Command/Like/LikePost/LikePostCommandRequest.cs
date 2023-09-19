namespace Chatverse.Application.Features.Command.Like.LikePost;

public class LikePostCommandRequest : IRequest<Common.Results.IResult>
{
    public int PostId { get; set; }
}
public class LikePostCommandHandler : IRequestHandler<LikePostCommandRequest, Common.Results.IResult>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;

    public LikePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<Common.Results.IResult> Handle(LikePostCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        Domain.Entities.Like newLike = new Domain.Entities.Like()
        {
            AppUserId = currentUser.Id,
            PostId = request.PostId
        };
        await _context.Likes.AddAsync(newLike);
        await _context.SaveChangesAsync(cancellationToken);
        return new Result(true, "complated");
    }
}
