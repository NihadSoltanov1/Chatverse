namespace Chatverse.Application.Features.Query.AppUser.GetAllUsers;

public class GetAllUsersQueryResponse
{
    public string UserId { get; set; }
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string? ProfilePicture { get; set; } 
    public string? BackgroundPicture { get; set; }
}
