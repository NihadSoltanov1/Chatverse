namespace Chatverse.Application.Features.Command.Story.DeleteStory;

public class DeleteStoryCommandRequest : IRequest<IDataResult<DeleteStoryCommandRequest>>
{
    public int Id { get; set; }
}
