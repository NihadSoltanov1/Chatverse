namespace Chatverse.Application.Features.Command.Friendship.RemoveFriend;

public class RemoveFriendCommandRequest : IRequest<Common.Results.IResult>
{
    public string Id { get; set; }
}
