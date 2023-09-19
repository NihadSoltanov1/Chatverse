namespace Chatverse.Domain.Entities;
    public class Post : BaseAuditableEntity
    {
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public string? Content { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<PostImage> PostImages { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public bool State { get; set; }

    }

