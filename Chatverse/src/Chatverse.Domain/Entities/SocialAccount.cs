namespace Chatverse.Domain.Entities;
    public class SocialAccount : BaseAuditableEntity
    {
        public string Category { get; set; }
        public string? Link { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
