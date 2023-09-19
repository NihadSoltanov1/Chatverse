namespace Chatverse.Domain.Entities;
    public class Friendship : BaseAuditableEntity
    {
        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        public bool Accept { get; set; } = false;
        public AppUser Sender { get; set; }
        public AppUser Receiver { get; set; }
    }

