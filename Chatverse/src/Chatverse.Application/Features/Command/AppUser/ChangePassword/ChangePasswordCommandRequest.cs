namespace Chatverse.Application.Features.Command.AppUser.ChangePassword;

public class ChangePasswordCommandRequest : IRequest<Common.Results.IResult>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
