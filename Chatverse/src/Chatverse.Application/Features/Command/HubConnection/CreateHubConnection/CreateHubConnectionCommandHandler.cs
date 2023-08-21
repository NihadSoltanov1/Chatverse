using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.HubConnection.CreateHubConnection
{
    public class CreateHubConnectionCommandHandler : IRequestHandler<CreateHubConnectionCommandRequest, CreateHubConnectionCommandResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        public CreateHubConnectionCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager)
        {
            _currentUserService = currentUserService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<CreateHubConnectionCommandResponse> Handle(CreateHubConnectionCommandRequest request, CancellationToken cancellationToken)
        {

            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            if(request.ConnectionId != null)
            {
                Domain.Entities.HubConnection hubConnection = new Domain.Entities.HubConnection()
                {
                    ConnectionId = request.ConnectionId,
                    Username = currentUser.UserName
                };
                _context.HubConnections.Add(hubConnection);
                await _context.SaveChangesAsync(cancellationToken);
                return new CreateHubConnectionCommandResponse()
                {
                    Id = currentUser.Id,
                    Username = currentUser.UserName
                };
            }
            throw new Exception();
        }
    }
}
