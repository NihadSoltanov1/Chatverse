namespace Chatverse.Application.Features.Command.Post.DeletePost;

public class DeletePostCommandRequest : IRequest<IDataResult<List<DeletePostCommandResponse>>>
{
    public int Id { get; set; }
}
