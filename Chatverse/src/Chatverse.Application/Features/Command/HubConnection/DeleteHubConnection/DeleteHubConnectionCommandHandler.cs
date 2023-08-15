using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection
{
    public class DeleteHubConnectionCommandHandler : IRequestHandler<DeleteHubConnectionCommandRequest, IResult>
    {
        private readonly IApplicationDbContext _context;

        public DeleteHubConnectionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(DeleteHubConnectionCommandRequest request, CancellationToken cancellationToken)
        {
           if(request.ConnecitionId != null)
            {
                var hubConnection = _context.HubConnections.FirstOrDefault(con => con.ConnectionId == request.ConnecitionId);
                _context.HubConnections.Remove(hubConnection);
                await _context.SaveChangesAsync(cancellationToken);
                return new Result(true, "success");
            }
            return new Result(false, "failed");


        }
    }
}
