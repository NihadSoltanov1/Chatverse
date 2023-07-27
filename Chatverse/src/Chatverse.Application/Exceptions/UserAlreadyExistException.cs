using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException() : base("This user already exist")
        {
        }

        public UserAlreadyExistException(string? message) : base(message)
        {
        }

        public UserAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
