using Shared.ValueObject.Domain;

namespace Shared.Aggregate.Message.ValueObject.Domain
{
    public class AggregateMessageAggregateOccurredAt : DateTimeValueObject
    {
        public AggregateMessageAggregateOccurredAt() : base()
        {
        }

        public AggregateMessageAggregateOccurredAt(DateTime value) : base(value)
        {
        }

        public AggregateMessageAggregateOccurredAt(int value) : base(value)
        {
        }

        public AggregateMessageAggregateOccurredAt(float value) : base(value)
        {
        }

        public AggregateMessageAggregateOccurredAt(string value) : base(value)
        {
        }
    }
}
