namespace Chatverse.Application.Exceptions;

public class AlreadyFriendEachOtherException : Exception
{
    public AlreadyFriendEachOtherException() : base("You are friend with this user")
    {
    }

    public AlreadyFriendEachOtherException(string? message) : base(message)
    {
    }

    public AlreadyFriendEachOtherException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
