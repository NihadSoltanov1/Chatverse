namespace Chatverse.Application.Features.Command.Story.DeleteStory;

public class DeleteStoryCommandHandler : IRequestHandler<DeleteStoryCommandRequest, IDataResult<DeleteStoryCommandRequest>>
{
    private readonly IApplicationDbContext _context;

    public DeleteStoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IDataResult<DeleteStoryCommandRequest>> Handle(DeleteStoryCommandRequest request, CancellationToken cancellationToken)
    {
        var story = _context.Stories.FirstOrDefault(x => x.Id == request.Id);
        if (story is not null)
        {
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync(cancellationToken);
            return new SuccessDataResult<DeleteStoryCommandRequest>(request, "Story added successfully");
        }
        return new SuccessDataResult<DeleteStoryCommandRequest>(request, "Story added successfully");
    }
}
