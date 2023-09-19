namespace Chatverse.Application.Common.Interfaces;
    public interface IEmailService
    {
        void SendMail(string email, string subject, string content);
        Task<Common.Results.IResult> ConfirmEmail(string userId, string token);
    }

