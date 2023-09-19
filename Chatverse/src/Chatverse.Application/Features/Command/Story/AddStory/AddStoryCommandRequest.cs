namespace Chatverse.Application.Features.Command.Story.AddStory;

public class AddStoryCommandRequest : IRequest<IDataResult<AddStoryCommandRequest>>
{
    public string Media { get; set; }
}
