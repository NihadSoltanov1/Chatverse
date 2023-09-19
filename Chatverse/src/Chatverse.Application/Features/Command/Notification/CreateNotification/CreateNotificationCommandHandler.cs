namespace Chatverse.Application.Features.Command.Notification.CreateNotification;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommandRequest, CreateNotificationCommandResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    public CreateNotificationCommandHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context, IMediator mediator)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _context = context;
        _mediator = mediator;
    }

    public async Task<CreateNotificationCommandResponse> Handle(CreateNotificationCommandRequest request, CancellationToken cancellationToken)
    {
        var senderCurrentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        var currentUser = await _userManager.FindByIdAsync(request.CurrentUserId);
        var category = _context.NotificationCategories.FirstOrDefault(c => c.Name == request.CategoryName);
        Domain.Entities.Notification notification = new Domain.Entities.Notification();
        switch (request.CategoryName)
        {
            case "FR": notification.Content = "sent you a friend request."; break;
            case "WC":
                {
                    notification.Content = $"write a comment on your post: '{request.CommentContent}...'";
                    notification.PostId = request.PostId;
                    notification.CommentId = request.CommentId;
                    break;
                }
            case "SP":
                {
                    notification.Content = "shared a new post. Check your friend's profile to see posts.";
                    notification.PostId = request.PostId;
                    break;
                }
            case "LP":
                {
                    notification.Content = "liked your post: Says to thanks to your friend.";
                    break;
                }
        }
        notification.CurrentUserId = currentUser.Id;
        notification.SenderUserId = senderCurrentUser.Id;
        notification.CategoryId = category.Id;
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);
     
        return new CreateNotificationCommandResponse();
    }
}
