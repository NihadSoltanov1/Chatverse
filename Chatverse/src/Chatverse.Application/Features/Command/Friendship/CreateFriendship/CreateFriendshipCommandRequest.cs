namespace Chatverse.Application.Features.Command.Friendship.CreateFriendship;

public record CreateFriendshipCommandRequest : IRequest<IDataResult<CreateFriendshipCommandRequest>>
{
    public string ReceiverId { get; set; }
}
