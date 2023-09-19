using static Azure.Core.HttpHeader;

namespace Chatverse.Infrastructure.Services;

public class EmailConfirmService : IEmailService
{
    private readonly UserManager<AppUser> _userManager;

    public EmailConfirmService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
 

    public void SendMail(string email, string subject, string content)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("mr.nihadsoltanov@gmail.com", "jytozsvpzuhbrqds"),
            EnableSsl = true,
        };
        MailMessage mailMessage = new MailMessage("mr.nihadsoltanov@gmail.com",email,subject,content);
        mailMessage.IsBodyHtml = true;
        smtpClient.Send(mailMessage);
    }

    public async Task<Application.Common.Results.IResult> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)  throw new NotFoundException("User not found");
        var decodedToken = WebEncoders.Base64UrlDecode(token);
        string normalToken = Encoding.UTF8.GetString(decodedToken);
        var result = await _userManager.ConfirmEmailAsync(user, normalToken);
        if(result.Succeeded) return new Result(true, "Email confirm succesfully");
        return new Result(false, "Please, confirm your account. Check your email");
    }
}
