using Shared.Aggregate.Domain;

namespace Shared.Exception.Domain
{
    public abstract class IAggregateException : System.Exception, IException, IAggregate
    {
        public Guid AggregateId { get; }
        public Guid AggregateTypeId { get; }
        public int AggregateCode { get; }
        public DateTime AggregateOccurredAt { get; }

        protected IAggregateException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string? message, System.Exception? inner) : base(message, inner)
        {
            AggregateId = aggregateId;
            AggregateTypeId = aggregateTypeId;
            AggregateCode = aggregateCode;
            AggregateOccurredAt = aggregateOccurredAt;
        }
    }
}
