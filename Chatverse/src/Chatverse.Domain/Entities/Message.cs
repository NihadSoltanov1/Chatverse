using Chatverse.Domain.Common;
using Chatverse.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Domain.Entities
{
    public class Message : BaseAuditableEntity
    {

        public string Content { get; set; }
        [ForeignKey("MessageSender")]
        public string SenderId { get; set; }
        [ForeignKey("MessageReceiver")]
        public string ReceiverId { get; set; }

        public AppUser MessageSender { get; set; }
        public AppUser MessageReceiver { get; set; }


    }
}
