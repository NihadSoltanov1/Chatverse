namespace Chatverse.Application.Features.Command.HubConnection.DeleteHubConnection;

public class DeleteHubConnectionCommandRequest : IRequest<Common.Results.IResult>
{
    public string ConnecitionId { get; set; }
}
