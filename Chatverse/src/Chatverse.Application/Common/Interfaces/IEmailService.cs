using Chatverse.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Common.Interfaces
{
    public interface IEmailService
    {
        void SendMail(string email, string subject, string content);
        Task<IResult> ConfirmEmail(string userId, string token);
    }
}
