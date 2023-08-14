using Chatverse.Domain.Common;
using Chatverse.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Chatverse.Domain.Entities
{
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
}
