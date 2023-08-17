using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Exceptions
{
    public class ChangePasswordException : Exception
    {
        public ChangePasswordException() : base("")
        {
        }

        public ChangePasswordException(string? message) : base(message)
        {
        }

        public ChangePasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
