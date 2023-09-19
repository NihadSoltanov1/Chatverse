namespace Chatverse.Application.Features.Command.Friendship.DeleteFriendshipRequest;

public class DeleteFriendshipRequestCommandHandler : IRequestHandler<DeleteFriendshipRequestCommandRequest, Common.Results.IResult>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<Domain.Identity.AppUser> _userManger;
    private readonly ICurrentUserService _currentUserService;

    public DeleteFriendshipRequestCommandHandler(IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManger, ICurrentUserService currentUserService)
    {
        _context = context;
        _userManger = userManger;
        _currentUserService = currentUserService;
    }

    public async Task<Common.Results.IResult> Handle(DeleteFriendshipRequestCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManger.FindByNameAsync(_currentUserService.UserName);
        var friendship = _context.Friendships.FirstOrDefault(x=>x.Id == request.FriendshipId);
        _context.Friendships.Remove(friendship);
        await _context.SaveChangesAsync(cancellationToken);
        return new Result(true, "Remove fried request succesfully");
    }
}
