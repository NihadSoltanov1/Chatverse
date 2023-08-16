namespace Chatverse.UI.ViewModels.Notifications
{
    public class GetUserNotificationViewModel
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public string? FullName { get; set; }
        public string? CategoryName { get; set; }
        public string? Icon { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
