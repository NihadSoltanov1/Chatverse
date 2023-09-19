namespace Chatverse.Application.Features.Command.Like.UnlikePost;

public class UnlikePostCommandRequest : IRequest<Common.Results.IResult>
{
    public int Id { get; set; }
}
