using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Notification.CreateNotification
{
    public class CreateNotificationCommandRequest : IRequest<CreateNotificationCommandResponse>
    {
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string CategoryName { get; set; }
        public string CurrentUserId { get; set; } 
    }
}
