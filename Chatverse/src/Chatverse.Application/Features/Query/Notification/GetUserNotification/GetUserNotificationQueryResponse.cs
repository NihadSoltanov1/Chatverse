namespace Chatverse.Application.Features.Query.Notification.GetUserNotification;

public class GetUserNotificationQueryResponse
{
    public int? Id { get; set; }
    public string? Content { get; set; }
    public string? FullName { get; set; }
    public string? CategoryName { get; set; }
    public string? Icon { get; set; }
    public DateTime CreateDate { get; set; }
}
