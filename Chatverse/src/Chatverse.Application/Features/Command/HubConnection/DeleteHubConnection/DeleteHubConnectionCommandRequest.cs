using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection
{
    public class DeleteHubConnectionCommandRequest : IRequest<IResult>
    {
        public string ConnecitionId { get; set; }
    }
}
