namespace Chatverse.Domain.Entities;
    public class Message
    {
        public Message()
        {
            SendDate = DateTime.UtcNow;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime SendDate { get; set; }

    }

