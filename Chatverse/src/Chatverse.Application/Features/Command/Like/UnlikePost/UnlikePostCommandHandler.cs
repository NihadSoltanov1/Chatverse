namespace Chatverse.Application.Features.Command.Like.UnlikePost;

public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommandRequest, Common.Results.IResult>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;

    public UnlikePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<Common.Results.IResult> Handle(UnlikePostCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        var postLike = _context.Likes.FirstOrDefault(l => l.PostId == request.Id && l.AppUserId == currentUser.Id);
        _context.Likes.Remove(postLike);
        await _context.SaveChangesAsync(cancellationToken);
        return new Result(true, "Complated");
    }
}
