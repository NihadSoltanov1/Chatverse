using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Domain.Entities
{
    public class NotificationCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
