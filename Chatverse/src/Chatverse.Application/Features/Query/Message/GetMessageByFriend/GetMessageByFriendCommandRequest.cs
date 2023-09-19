namespace Chatverse.Application.Features.Query.Message.GetMessageByFriend;

public class GetMessageByFriendCommandRequest : IRequest<List<GetMessageByFriendCommandResponse>>
{
    public string FriendId { get; set; }
    public string? CurrentUserId { get; set; }
}
