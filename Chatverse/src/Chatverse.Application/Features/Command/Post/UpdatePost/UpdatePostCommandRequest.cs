namespace Chatverse.Application.Features.Command.Post.UpdatePost;

public class UpdatePostCommandRequest : IRequest<IDataResult<List<UpdatePostCommandResponse>>>
{
    public int UpdatePostId { get; set; }
    public string? UpdateContent { get; set; }
    public List<string>? UpdateMedia { get; set; }
}
