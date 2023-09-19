namespace Chatverse.Application.Features.Query.Comment.GetCommentByPostId;

public class GetCommentByPostIdQueryRequest : IRequest<GetCommentByPostIdQueryResponse>
{
    public int PostId { get; set; }
}
