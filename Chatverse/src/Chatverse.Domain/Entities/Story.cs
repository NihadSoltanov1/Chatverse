namespace Chatverse.Domain.Entities;
    public class Story : BaseAuditableEntity
    {
        public string Media { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
