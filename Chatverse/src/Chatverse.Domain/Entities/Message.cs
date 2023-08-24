using Chatverse.Domain.Common;
using Chatverse.Domain.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Chatverse.Domain.Entities
{
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
}
