namespace Chatverse.Application.Exceptions;

public class WaitingAcceptToFriendRequestException : Exception
{
    public WaitingAcceptToFriendRequestException() : base("You have send request to user. Wait to his accepting")
    {
    }

    public WaitingAcceptToFriendRequestException(string? message) : base(message)
    {
    }

    public WaitingAcceptToFriendRequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
