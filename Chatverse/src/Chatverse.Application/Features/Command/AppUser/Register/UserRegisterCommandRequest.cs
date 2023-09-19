namespace Chatverse.Application.Features.Command.AppUser.Register;

public record UserRegisterCommandRequest : IRequest<Common.Results.IResult>
{
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public bool IsAgree { get; set; }
}
