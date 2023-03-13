using Shared.ValueObject.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Domain
{
    public class AggregateExceptionAggregateId : UuidValueObject
    {
        public AggregateExceptionAggregateId() : base()
        {
        }

        public AggregateExceptionAggregateId(Guid value) : base(value)
        {
        }

        public AggregateExceptionAggregateId(string value) : base(value)
        {
        }
    }
}
