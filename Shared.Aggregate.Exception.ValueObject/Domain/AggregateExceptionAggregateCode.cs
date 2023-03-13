using Shared.ValueObject.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Domain
{
    public class AggregateExceptionAggregateCode : IntValueObject
    {
        public AggregateExceptionAggregateCode() : base(500)
        {
        }
        public AggregateExceptionAggregateCode(int value) : base(value)
        {
        }

        public AggregateExceptionAggregateCode(float value) : base(value)
        {
        }

        public AggregateExceptionAggregateCode(string value) : base(value)
        {
        }

        public AggregateExceptionAggregateCode(bool value) : base(value)
        {
        }

        public AggregateExceptionAggregateCode(DateTime value) : base(value)
        {
        }
    }
}
