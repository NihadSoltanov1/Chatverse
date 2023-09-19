namespace Chatverse.Domain.Entities;
    public class Comment : BaseAuditableEntity
    {
        public string Content { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }

