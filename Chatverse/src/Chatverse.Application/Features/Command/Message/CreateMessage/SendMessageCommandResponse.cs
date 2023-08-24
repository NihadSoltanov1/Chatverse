using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Message.CreateMessage
{
    public class SendMessageCommandResponse
    {
        public string? MessageId { get; set; }
        public string? SenderId { get; set; }
        public string? SenderUsername { get; set; }
        public string? SenderProfilePicture { get; set; }
        public string? ReceiverId { get; set; }
        public string? ReceiverUsername { get; set; }
        public string? ReceiverProfilePicture { get; set; }
        public string?  Content { get; set; }
        public string? Image { get; set; }
        public DateTime SendDate { get; set; }
    }
}
