namespace Shared.Exception.Domain;

public abstract class IException : System.Exception
{
    protected IException() { }
    protected IException(string? message) : base(message) { }
    protected IException(string? message, System.Exception? innerException) : base(message, innerException) { }
}