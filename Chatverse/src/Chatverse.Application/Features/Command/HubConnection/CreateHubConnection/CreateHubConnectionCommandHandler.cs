namespace Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;

public class CreateHubConnectionCommandHandler : IRequestHandler<CreateHubConnectionCommandRequest, CreateHubConnectionCommandResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;
    public CreateHubConnectionCommandHandler(IApplicationDbContext context, UserManager<Domain.Identity.AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<CreateHubConnectionCommandResponse> Handle(CreateHubConnectionCommandRequest request, CancellationToken cancellationToken)
    {

        var currentUser = await _userManager.FindByIdAsync(request.UserId);
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
