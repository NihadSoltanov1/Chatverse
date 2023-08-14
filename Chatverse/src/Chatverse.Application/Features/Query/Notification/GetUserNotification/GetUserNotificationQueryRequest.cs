using Chatverse.Application.Features.Query.Notification.GetUserNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Notification.GetUserNotificationQuery
{
    public class GetUserNotificationQueryRequest : IRequest<List<GetUserNotificationQueryResponse>>
    {
    }
}
