namespace Chatverse.Domain.Entities;

    public class NotificationCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }

