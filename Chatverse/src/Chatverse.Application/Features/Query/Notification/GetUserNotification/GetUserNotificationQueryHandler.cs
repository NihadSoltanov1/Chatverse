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
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);


            var allNotifications = await _context.Notifications
                .Where(x => x.CurrentUserId == currentUser.Id)
                .ToListAsync();

            List<GetUserNotificationQueryResponse> getUserNotificationQueryResponses = new List<GetUserNotificationQueryResponse>();

            if (allNotifications is not null)
            {
                var latestNotifications = allNotifications.OrderByDescending(n => n.CreatedDate).Take(4).ToList();
                foreach(var notification in latestNotifications)
                {
                    var category = await _context.NotificationCategories
                        .FirstOrDefaultAsync(n => n.Id == notification.CategoryId);
                    var requester = await _userManager.FindByIdAsync(notification.SenderUserId);

                    GetUserNotificationQueryResponse notifc =  new GetUserNotificationQueryResponse()
                    {
                        CategoryName = category?.Name,
                        Content = notification.Content,
                        Id = notification.Id,
                        RequestFriend = requester?.FullName
                    };
                    getUserNotificationQueryResponses.Add(notifc);
                }
               
            }

            return getUserNotificationQueryResponses;
        }
    }
}