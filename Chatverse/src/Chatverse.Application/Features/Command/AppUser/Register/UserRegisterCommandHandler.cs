namespace Chatverse.Application.Features.Command.AppUser.Register;

public record UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, Common.Results.IResult>
{
    private readonly UserManager<Chatverse.Domain.Identity.AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _email;
    public UserRegisterCommandHandler(UserManager<Domain.Identity.AppUser> userManager, IConfiguration configuration, IEmailService email)
    {
        _userManager = userManager;
        _configuration = configuration;
        _email = email;
    }

    public async Task<Common.Results.IResult> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var anyUser = await _userManager.FindByEmailAsync(request.Email);

        if (anyUser is not null) throw new UserAlreadyExistException("This email already exist");
        anyUser = await _userManager.FindByNameAsync(request.Username);
        if (anyUser is not null) throw new UserAlreadyExistException("This username already exist");
        Domain.Identity.AppUser newUser = new()
        {
            FullName = request.FullName,
            UserName = request.Username,
            Email = request.Email,
            ProfilePicture = "profilepictures/defaultprofilepicture.png"
        };

        IdentityResult identityResult = await _userManager.CreateAsync(newUser, request.Password);      
        if (!identityResult.Succeeded) throw new UserCreateFailedException("An unexpected error occurred while creating the user!");
        string confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
        var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
        string url = $"{_configuration["ApiStatic:MvcUrl"]}/auth/confirmemail?userid={newUser.Id}&token={validEmailToken}";
        _email.SendMail(newUser.Email, "Confirm Your Email", $"<h1>Welcome ChatVerse</h1>" + $"<div style=\"margin: 0; padding: 0; background-color: #f4f4f4; font-family: Arial, sans-serif;\">\r\n    <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"600\" style=\"border-collapse: collapse;\">\r\n        <tr>\r\n            <td bgcolor=\"#3498db\" style=\"padding: 20px; text-align: center;\">\r\n                <h1 style=\"color: #ffffff;\">Email Confirmation</h1>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td bgcolor=\"#ffffff\" style=\"padding: 20px;\">\r\n                <p>Thank you for signing up! Please click the button below to confirm your email address:</p>\r\n                <p style=\"text-align: center;\"><a href=\"{url}\" style=\"display: inline-block; padding: 10px 20px; background-color: #3498db; color: #ffffff; text-decoration: none; border-radius: 3px;\">Confirm Email</a></p>\r\n                <p>If you didn't sign up for our service, you can ignore this email.</p>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</div>");
        return new Result(true, "User created succesfully");
    }
}
