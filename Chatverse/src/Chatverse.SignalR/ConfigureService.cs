using Chatverse.Application.Common.Hubs;
using Chatverse.SignalR.HubsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.SignalR
{
    public static class ConfigureService
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INotificationHubService, NotificationHubService>();
            services.AddSignalR();
            return services;
        }
    }
}
