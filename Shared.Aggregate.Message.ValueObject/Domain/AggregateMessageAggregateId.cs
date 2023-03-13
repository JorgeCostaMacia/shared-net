using Shared.ValueObject.Domain;

namespace Shared.Aggregate.Message.ValueObject.Domain
{
    public class AggregateMessageAggregateId : UuidValueObject
    {
        public AggregateMessageAggregateId() : base()
        {
        }

        public AggregateMessageAggregateId(Guid value) : base(value)
        {
        }

        public AggregateMessageAggregateId(string value) : base(value)
        {
        }
    }
}
