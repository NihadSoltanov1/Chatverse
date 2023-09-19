namespace Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection;

public class DeleteHubConnectionCommandHandler : IRequestHandler<DeleteHubConnectionCommandRequest, Common.Results.IResult>
{
    private readonly IApplicationDbContext _context;

    public DeleteHubConnectionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Common.Results.IResult> Handle(DeleteHubConnectionCommandRequest request, CancellationToken cancellationToken)
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
