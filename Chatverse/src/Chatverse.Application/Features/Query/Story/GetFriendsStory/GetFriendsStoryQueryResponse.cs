namespace Chatverse.Application.Features.Query.Story.GetFriendsStory;

public class GetFriendsStoryQueryResponse
{
    public List<FriendsStory>? Stories { get; set; }
}
public class FriendsStory
{
    public string? FriendId { get; set; }
    public string? Username { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Media { get; set; }
}
