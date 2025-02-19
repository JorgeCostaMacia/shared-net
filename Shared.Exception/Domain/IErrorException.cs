using System.Collections.Immutable;

namespace Shared.Exception.Domain;

public abstract class IErrorException : IAggregateException
{
    public ImmutableList<string> Errors { get; init; }

    protected IErrorException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<string> errors) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException)
    {
        Errors = errors.ToImmutableList();
    }

    protected IErrorException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<string> errors) : base(aggregateId, aggregateTypeId, aggregateCode ?? 400, aggregateOccurredAt, $"{message ?? "Error Exception"} => {string.Join(",", errors)}", innerException)
    {
        Errors = errors.ToImmutableList();
    }
}