using Chatverse.Application.Common.Hubs;
using Chatverse.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Notification.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommandRequest, CreateNotificationCommandResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly IApplicationDbContext _context;
        private readonly INotificationHubService _notificationHubService;
        public CreateNotificationCommandHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context, INotificationHubService notificationHubService)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _context = context;
            _notificationHubService = notificationHubService;
        }

        public async Task<CreateNotificationCommandResponse> Handle(CreateNotificationCommandRequest request, CancellationToken cancellationToken)
        {
            var senderCurrentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var currentUser = await _userManager.FindByIdAsync(request.CurrentUserId);
            var category = _context.NotificationCategories.FirstOrDefault(c => c.Name == request.CategoryName);
            Domain.Entities.Notification notification = new Domain.Entities.Notification();
            switch (request.CategoryName)
            {
                case "FR": notification.Content = "Sent you a friend request."; break;
                case "WC":
                    {
                        notification.Content = "Wrote a comment on your post.";
                        notification.PostId = request.PostId;
                        notification.CommentId = request.CommentId;
                        break;
                    }
                case "SP":
                    {
                        notification.Content = "Shared a new post.";
                        notification.PostId = request.PostId;
                        break;
                    }
            }
            notification.CurrentUserId = currentUser.Id;
            notification.SenderUserId = senderCurrentUser.Id;
            notification.CategoryId = category.Id;
            _context.Notifications.Add(notification);
           await _context.SaveChangesAsync(cancellationToken);
           await _notificationHubService.NotificationAddedMessageAsync("urun eklendi");
            return new CreateNotificationCommandResponse();
        }
    }
}
