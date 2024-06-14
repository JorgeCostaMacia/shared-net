using System.Collections.Immutable;

namespace Shared.Exception.Domain;

public class IErrorException : IAggregateException
{
    public IImmutableList<string> Errors { get; init; }

    protected IErrorException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<string> errors) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
    {
        Errors = errors.ToImmutableList();
    }

    protected IErrorException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<string> errors) : base(Guid.NewGuid(), aggregateTypeId, 500, $"{message} => {string.Join(",", errors)}", inner)
    {
        Errors = errors.ToImmutableList();
    }
}
