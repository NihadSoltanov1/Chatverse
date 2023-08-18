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
    public class SocialAccount : BaseAuditableEntity
    {
        public string Category { get; set; }
        public string? Link { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
