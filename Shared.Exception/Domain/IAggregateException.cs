using Shared.Aggregate.Domain;

namespace Shared.Exception.Domain
{
    public abstract class IAggregateException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string? message, System.Exception? inner) : System.Exception(message, inner), IException, IAggregate
    {
        public Guid AggregateId { get; init; } = aggregateId;
        public Guid AggregateTypeId { get; init; } = aggregateTypeId;
        public int AggregateCode { get; init; } = aggregateCode;
        public DateTime AggregateOccurredAt { get; init; } = aggregateOccurredAt;
    }
}
