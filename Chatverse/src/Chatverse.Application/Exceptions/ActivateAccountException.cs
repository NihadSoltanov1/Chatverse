using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Exceptions
{
    public class ActivateAccountException : Exception
    {
        public ActivateAccountException() : base("")
        {
        }

        public ActivateAccountException(string? message) : base(message)
        {
        }

        public ActivateAccountException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
