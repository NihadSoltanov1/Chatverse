namespace Chatverse.Application.Features.Command.AppUser.ForgetPassword.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<Common.Results.IResult>
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
