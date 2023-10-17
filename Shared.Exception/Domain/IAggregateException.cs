using Shared.Aggregate.Domain;

namespace Shared.Exception.Domain
{
    public abstract class IAggregateException : System.Exception, IException, IAggregate
    {
        public Guid AggregateId { get; init; }
        public Guid AggregateTypeId { get; init; }
        public int AggregateCode { get; init; }
        public DateTime AggregateOccurredAt { get; init; }

        protected IAggregateException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string? message, System.Exception? inner) : base(message, inner)
        {
            AggregateId = aggregateId;
            AggregateTypeId = aggregateTypeId;
            AggregateCode = aggregateCode;
            AggregateOccurredAt = aggregateOccurredAt;
        }
    }
}
