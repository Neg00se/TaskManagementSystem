namespace ServiceLayer.Validation;
public class EmailOrUsernameAlreadyExistException : Exception
{
    public EmailOrUsernameAlreadyExistException()
    {
    }

    public EmailOrUsernameAlreadyExistException(string? message) : base(message)
    {
    }

    public EmailOrUsernameAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
