namespace Shared.Exception.Domain;

public abstract class INotFoundException : IException
{
    protected INotFoundException(Guid id, Guid typeId, int code, DateTime occurredAt, string message, System.Exception? inner) : base(id, typeId, code, occurredAt, message, inner) { }
    protected INotFoundException(Guid typeId, string message, System.Exception? inner) : base(Guid.NewGuid(), typeId, 404, message, inner) { }
}

