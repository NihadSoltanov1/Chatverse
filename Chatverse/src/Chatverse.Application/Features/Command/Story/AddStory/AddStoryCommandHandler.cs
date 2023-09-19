namespace Chatverse.Application.Features.Command.Story.AddStory;

public class AddStoryCommandHandler : IRequestHandler<AddStoryCommandRequest, IDataResult<AddStoryCommandRequest>>
{
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private readonly IStoryScheduleService _storySchedule;
    public AddStoryCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ICurrentUserService currentUserService, IApplicationDbContext context, IStoryScheduleService storySchedule)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
        _context = context;
        _storySchedule = storySchedule;
    }

    public async Task<IDataResult<AddStoryCommandRequest>> Handle(AddStoryCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        Domain.Entities.Story newStory = new Domain.Entities.Story()
        {
            Media = request.Media,
            UserId = currentUser.Id
        };
        await _context.Stories.AddAsync(newStory);
        await _context.SaveChangesAsync(cancellationToken);
        _storySchedule.ScheduleDataCleanup(newStory.Id);
        return new SuccessDataResult<AddStoryCommandRequest>(request, "Story added successfully");
    }
}
