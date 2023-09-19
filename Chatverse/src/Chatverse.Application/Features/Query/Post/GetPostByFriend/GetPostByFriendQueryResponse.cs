namespace Chatverse.Application.Features.Query.Post.GetPostByFriend;

public record GetPostByFriendQueryResponse
{
    public List<GetFriendsPosts>? Posts { get; set; }
}

public class GetFriendsPosts
{
    public int? PostId { get; set; }
    public string? FullName { get; set; }
    public string? Content { get; set; }
    public string? FriendProfilePicture { get; set; }
    public List<string>? Media { get; set; }
    public List<GetCommentByPostId>? Comments { get; set; }
    public string? CurrentUser { get; set; }
    public DateTime? CreateDate { get; set; }
}
