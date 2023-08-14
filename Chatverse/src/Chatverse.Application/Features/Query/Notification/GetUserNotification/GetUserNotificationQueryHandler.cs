using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Features.Query.Notification.GetUserNotificationQuery;
using Chatverse.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Notification.GetUserNotification
{
    public class GetUserNotificationQueryHandler : IRequestHandler<GetUserNotificationQueryRequest, List<GetUserNotificationQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        public GetUserNotificationQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<List<GetUserNotificationQueryResponse>> Handle(GetUserNotificationQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUser =await _userManager.FindByNameAsync(_currentUserService.UserName);
            
           var allNotifications = await _context.Notifications.Where(x => x.CurrentUserId == currentUser.Id).ToListAsync();
            List<GetUserNotificationQueryResponse> getUserNotificationQueryResponses = new List<GetUserNotificationQueryResponse>();
            if (allNotifications is not null)
            {

                var tasks = allNotifications.Select(async notif =>
                {
                    var categoryName = (_context.NotificationCategories.FirstOrDefault(n => n.Id == notif.CategoryId))?.Name;
                    var requester = await _userManager.FindByIdAsync(notif.SenderUserId);
                    return new GetUserNotificationQueryResponse
                    {
                        CategoryName = categoryName,
                        Content = notif.Content,
                        Id = notif.Id,
                        RequestFriend = requester?.FullName
                    };
                });

                getUserNotificationQueryResponses.AddRange(await Task.WhenAll(tasks));

            }
            return getUserNotificationQueryResponses;
        }
    }
}
