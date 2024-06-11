using System.Collections.Immutable;

namespace Shared.Exception.Domain
{
    public class IErrorException(IEnumerable<string> errors, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : IAggregateException(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
    {
        public IImmutableList<string> Errors { get; init; } = errors.ToImmutableList();

        public IErrorException(IEnumerable<string> errors, Guid aggregateTypeId, string message, System.Exception? inner) : this(errors, Guid.NewGuid(), aggregateTypeId, 500, DateTime.UtcNow, $"{message} => {string.Join(",", errors)}", inner) { }
    }
}
