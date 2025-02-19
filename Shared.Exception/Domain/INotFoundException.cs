namespace Shared.Exception.Domain;

public abstract class INotFoundException : IAggregateException
{
    protected INotFoundException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException) { }
    protected INotFoundException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode ?? 404, aggregateOccurredAt, $"{message ?? "Not Found Exception"}", innerException) { }
}