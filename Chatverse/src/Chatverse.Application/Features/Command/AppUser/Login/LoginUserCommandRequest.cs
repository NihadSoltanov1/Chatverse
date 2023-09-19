namespace Chatverse.Application.Features.Command.AppUser.Login;

public record LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
