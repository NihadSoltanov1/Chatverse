namespace Chatverse.Application.Features.Command.AppUser.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly ITokenHandler _tokenHandler;
    private readonly UserManager<Chatverse.Domain.Identity.AppUser> _userManager;

    public LoginUserCommandHandler(ITokenHandler tokenHandler, UserManager<Domain.Identity.AppUser> userManager)
    {
        _tokenHandler = tokenHandler;
        _userManager = userManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        if (user is null)
        {
            user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user is null) throw new NotFoundException("User not found.");
        }
        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!result) throw new AuthenticationErrorException();
        if (user.EmailConfirmed == false) throw new ActivateAccountException("Please check your email and activate your account");
        var accessToken = _tokenHandler.CreateAccessToken(60,user);

        return new LoginUserCommandResponse()
        {
            Token = accessToken
        };

    }
}
