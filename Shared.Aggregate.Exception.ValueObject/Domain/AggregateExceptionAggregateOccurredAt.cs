using Shared.ValueObject.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Domain
{
    public class AggregateExceptionAggregateOccurredAt : DateTimeValueObject
    {
        public AggregateExceptionAggregateOccurredAt() : base()
        {
        }

        public AggregateExceptionAggregateOccurredAt(DateTime value) : base(value)
        {
        }

        public AggregateExceptionAggregateOccurredAt(int value) : base(value)
        {
        }

        public AggregateExceptionAggregateOccurredAt(float value) : base(value)
        {
        }

        public AggregateExceptionAggregateOccurredAt(string value) : base(value)
        {
        }
    }
}
