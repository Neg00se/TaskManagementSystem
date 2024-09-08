namespace ServiceLayer.Validation;
public class UserMismatchException : Exception
{
    public UserMismatchException()
    {
    }

    public UserMismatchException(string? message) : base(message)
    {
    }

    public UserMismatchException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
