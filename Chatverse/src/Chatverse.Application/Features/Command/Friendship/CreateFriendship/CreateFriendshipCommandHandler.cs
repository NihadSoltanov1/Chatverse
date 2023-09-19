namespace Chatverse.Application.Features.Command.Friendship.CreateFriendship;

public class CreateFriendshipCommandHandler : IRequestHandler<CreateFriendshipCommandRequest, IDataResult<CreateFriendshipCommandRequest>>
{
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    public CreateFriendshipCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService, IApplicationDbContext context, IMediator mediator)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
        _context = context;
        _mediator = mediator;
    }

    public async Task<IDataResult<CreateFriendshipCommandRequest>> Handle(CreateFriendshipCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        var currentUserId = currentUser.Id;
        var receiverUser = await _userManager.FindByIdAsync(request.ReceiverId);
        if (receiverUser is null) throw new NotFoundException("This user can not found");
        var isFriend = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == currentUserId && x.ReceiverId == request.ReceiverId);

        if (isFriend is not null)
        {
            if (isFriend.Accept == false) throw new WaitingAcceptToFriendRequestException("You sent friend's request already.");
            throw new AlreadyFriendEachOtherException("You are already friend.");
        }
        var _isFriend = await _context.Friendships.FirstOrDefaultAsync(x => x.SenderId == request.ReceiverId && x.ReceiverId == currentUserId );
        if (_isFriend is not null)
        {
            if (_isFriend.Accept == false) throw new WaitingAcceptToFriendRequestException("You sent friend's request already.");
            throw new AlreadyFriendEachOtherException("You are already friend.");
        }

        Domain.Entities.Friendship newFriendship1 = new()
        {
            SenderId = currentUserId,
            ReceiverId = receiverUser.Id
        };
        await _context.Friendships.AddAsync(newFriendship1);
        await _context.SaveChangesAsync(cancellationToken);
     
        CreateNotificationCommandRequest createNotificationCommandRequest = new CreateNotificationCommandRequest()
        {
            CategoryName = "FR",
            CurrentUserId = newFriendship1.ReceiverId
        };
        await _mediator.Send(createNotificationCommandRequest);
        return new SuccessDataResult<CreateFriendshipCommandRequest>(request, "Send friend request successfully");


    }
}
