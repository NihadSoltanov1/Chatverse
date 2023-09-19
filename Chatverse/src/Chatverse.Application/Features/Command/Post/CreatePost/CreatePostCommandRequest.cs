namespace Chatverse.Application.Features.Command.Post.CreatePost;

public record CreatePostCommandRequest : IRequest<IDataResult<CreatePostCommandRequest>>
{
    public string? Content { get; set; }
    public List<string>? MediaLocation { get; set; }
}
