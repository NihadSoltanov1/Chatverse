namespace Chatverse.Application.Features.Command.Friendship.DeleteFriendshipRequest;

public class DeleteFriendshipRequestCommandRequest : IRequest<Common.Results.IResult>
{
    public int FriendshipId { get; set; }
}
