namespace Chatverse.Application.Features.Command.Comment.DeleteComment;

public class DeleteCommentCommandRequest : IRequest<IDataResult<DeleteCommentCommandRequest>>
{
    public int Id { get; set; }

}
