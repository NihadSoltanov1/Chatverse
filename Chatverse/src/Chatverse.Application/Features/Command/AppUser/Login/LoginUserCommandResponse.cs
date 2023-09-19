namespace Chatverse.Application.Features.Command.AppUser.Login;

public record LoginUserCommandResponse
{
    public TokenDto Token { get; set; }
}
