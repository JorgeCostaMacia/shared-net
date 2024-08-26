namespace Shared.Exception.Domain;

public abstract class IExistException : IException
{
    protected IExistException(Guid id, Guid typeId, int code, DateTime occurredAt, string message, System.Exception? inner) : base(id, typeId, code, occurredAt, message, inner) { }
    protected IExistException(Guid typeId, string message, System.Exception? inner) : base(Guid.NewGuid(), typeId, 409, message, inner) { }
}