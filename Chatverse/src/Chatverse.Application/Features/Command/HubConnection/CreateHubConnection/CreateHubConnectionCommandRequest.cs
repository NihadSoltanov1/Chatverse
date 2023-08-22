using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.HubConnection.CreateHubConnection
{
    public class CreateHubConnectionCommandRequest : IRequest<CreateHubConnectionCommandResponse>
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
