namespace Chatverse.Application.Features.Command.AppUser.ForgetPassword;

public class ForgetPasswordCommandRequest : IRequest<Common.Results.IResult>
{
    public string Email { get; set; }
}
