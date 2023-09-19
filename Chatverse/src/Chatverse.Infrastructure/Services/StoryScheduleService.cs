namespace Chatverse.Infrastructure.Services;

public class StoryScheduleService : IStoryScheduleService
{
    private readonly IMediator _mediator;

    public StoryScheduleService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public void DeleteStory(int id) {
        DeleteStoryCommandRequest deleteStoryCommandRequest = new DeleteStoryCommandRequest();
        deleteStoryCommandRequest.Id = id;
         _mediator.Send(deleteStoryCommandRequest);
    }
    public  void ScheduleDataCleanup(int id)
    {
      
        var cleanupTime = DateTime.Now.AddHours(24);
        BackgroundJob.Schedule(() => DeleteStory(id), cleanupTime);
    }
}
