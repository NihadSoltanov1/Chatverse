namespace Chatverse.Application.Features.Query.Post.GetPostById;

public class GetPostByIdQueryRequest : IRequest<GetPostByIdQueryResponse>
{
    public int Id { get; set; }
}
