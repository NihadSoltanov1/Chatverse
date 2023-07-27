using Chatverse.Domain.Entities;
using Chatverse.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chatverse.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? BackgroundPicture { get; set; }
        public Gender? UserGender { get; set; }
        public bool? State { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        [InverseProperty("Sender")]
        public ICollection<Friendship> SentFriendRequests { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<Friendship> ReceivedFriendRequests { get; set; }
    }
}
