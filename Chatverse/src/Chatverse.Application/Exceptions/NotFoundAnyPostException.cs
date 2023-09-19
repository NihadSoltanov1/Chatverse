namespace Chatverse.Application.Exceptions;

public class NotFoundAnyPostException : Exception
{
    public NotFoundAnyPostException() : base("")
    {
    }

    public NotFoundAnyPostException(string? message) : base(message)
    {
    }

    public NotFoundAnyPostException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
