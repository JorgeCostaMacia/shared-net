namespace Shared.Exception.Domain;

public abstract class IDomainException : System.Exception
{
    protected IDomainException() { }
    protected IDomainException(string? message) : base(message) { }
    protected IDomainException(string? message, System.Exception? innerException) : base(message, innerException) { }
}