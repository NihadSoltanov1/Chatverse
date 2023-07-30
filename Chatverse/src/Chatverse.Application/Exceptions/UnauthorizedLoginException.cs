using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Exceptions
{
    public class UnauthorizedLoginException : Exception
    {
        public UnauthorizedLoginException() : base("Please log in your account")
        {
        }

        public UnauthorizedLoginException(string? message) : base(message)
        {
        }

        public UnauthorizedLoginException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
