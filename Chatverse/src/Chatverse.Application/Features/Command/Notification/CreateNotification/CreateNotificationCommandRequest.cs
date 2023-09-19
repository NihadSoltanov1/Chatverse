namespace Chatverse.Application.Features.Command.Notification.CreateNotification;

public class CreateNotificationCommandRequest : IRequest<CreateNotificationCommandResponse>
{
    public int? PostId { get; set; }
    public int? CommentId { get; set; }
    public string? CommentContent { get; set; }
    public string CategoryName { get; set; }
    public string CurrentUserId { get; set; } 
}
