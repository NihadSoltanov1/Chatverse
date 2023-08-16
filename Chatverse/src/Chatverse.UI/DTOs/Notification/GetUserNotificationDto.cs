namespace Chatverse.UI.DTOs.Notification
{
    public class GetUserNotificationDto
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public string? FullName { get; set; }
        public string? CategoryName { get; set; }
        public string? Icon { get; set; }
        public string? DayYear { get; set; }
        public string? Hour { get; set; }
    }
}
