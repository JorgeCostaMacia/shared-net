using Shared.Aggregate.Domain;

namespace Shared.Aggregate.Exception.Domain
{
    public abstract class IAggregateException : System.Exception, IAggregate
    {
        public Guid AggregateId { get; }

        public int AggregateCode { get; }

        public DateTime AggregateOccurredAt { get; }

        public IAggregateException(Guid aggregateId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception inner) : base(message, inner)
        {
            AggregateId = aggregateId;
            AggregateCode = aggregateCode;
            AggregateOccurredAt = aggregateOccurredAt;
        }

        public IAggregateException(Guid aggregateId, int aggregateCode, string message, System.Exception inner) : base(message, inner)
        {
            AggregateId = aggregateId;
            AggregateCode = aggregateCode;
            AggregateOccurredAt = new DateTime();
        }

        public IAggregateException(Guid aggregateId, int aggregateCode, string message) : base(message)
        {
            AggregateId = aggregateId;
            AggregateCode = aggregateCode;
            AggregateOccurredAt = new DateTime();
        }

        public IAggregateException(string message, System.Exception inner) : base(message, inner)
        {
            AggregateId = new Guid("2c1fd777-1299-4579-a26d-9d29ff40d7d7");
            AggregateCode = 500;
            AggregateOccurredAt = new DateTime();
        }

        public IAggregateException(string message) : base(message)
        {
            AggregateId = new Guid("2c1fd777-1299-4579-a26d-9d29ff40d7d7");
            AggregateCode = 500;
            AggregateOccurredAt = new DateTime();
        }

        public IAggregateException()
        {
            AggregateId = new Guid("2c1fd777-1299-4579-a26d-9d29ff40d7d7");
            AggregateCode = 500;
            AggregateOccurredAt = new DateTime();
        }
    }
}
