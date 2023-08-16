﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Common.Hubs
{
    public interface INotificationHubService
    {
        Task SendNotificationToClient(string message, string username, string connectionId);

    }
}
