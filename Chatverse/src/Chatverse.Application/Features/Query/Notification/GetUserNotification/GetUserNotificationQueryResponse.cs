using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Notification.GetUserNotification
{
    public class GetUserNotificationQueryResponse
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public string? RequestFriend { get; set; }
        public string? CategoryName { get; set; }
    }
}
