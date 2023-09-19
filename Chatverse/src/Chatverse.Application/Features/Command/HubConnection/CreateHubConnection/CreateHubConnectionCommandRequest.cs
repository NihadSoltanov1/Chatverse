namespace Chatverse.Application.Features.Command.HubConnection.CreateHubConnection;

public class CreateHubConnectionCommandRequest : IRequest<CreateHubConnectionCommandResponse>
{
    public string UserId { get; set; }
    public string ConnectionId { get; set; }
}
