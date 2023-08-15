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
    public class Notification : BaseAuditableEntity
    {
      
        [ForeignKey("CurrentUser")]
        public string CurrentUserId { get; set; }
        [ForeignKey("SenderUser")]
        public string SenderUserId { get; set; }
        public string? MessageType { get; set; }
        public string Content { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        [ForeignKey("Comment")]
        public int? CommentId { get; set; }

        [ForeignKey("NotificationCategory")]
        public int CategoryId { get; set; }
        public NotificationCategory NotificationCategory { get; set; }
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
        public AppUser CurrentUser { get; set; }
        public AppUser SenderUser { get; set; }
    }
}
