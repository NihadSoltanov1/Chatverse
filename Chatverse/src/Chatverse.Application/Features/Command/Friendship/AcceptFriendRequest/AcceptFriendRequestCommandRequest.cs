namespace Chatverse.Application.Features.Command.Friendship.AcceptFriendRequest;

public class AcceptFriendRequestCommandRequest : IRequest<Common.Results.IResult>
{
    public int FrienshipId { get; set; }
}
