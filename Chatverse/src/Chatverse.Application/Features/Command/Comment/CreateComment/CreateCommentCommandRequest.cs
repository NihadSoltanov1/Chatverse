namespace Chatverse.Application.Features.Command.Comment.CreateComment;

public record CreateCommentCommandRequest : IRequest<IDataResult<CreateCommentCommandRequest>>
{
    public int PostId { get; set; }
    public string Content { get; set; }
}
