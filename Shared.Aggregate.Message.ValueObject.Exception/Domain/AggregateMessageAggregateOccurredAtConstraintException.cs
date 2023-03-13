using Shared.ValueObject.Exception.Domain;

namespace Shared.Aggregate.Message.ValueObject.Domain
{
    public class AggregateMessageAggregateOccurredAtConstraintException : DateTimeValueObjectConstraintException
    {
        public AggregateMessageAggregateOccurredAtConstraintException(AggregateMessageAggregateOccurredAt value, List<string> constraints) : base("AggregateOccurrerdAt", value, constraints, new Guid("381c12d9-9477-4646-aaed-e7a00584832c"), "AggregateMessage.AggregateOccurrerdAt Constraint Exception")
        {
        }

        public AggregateMessageAggregateOccurredAtConstraintException(AggregateMessageAggregateOccurredAt value, List<string> constraints, System.Exception inner) : base("AggregateOccurrerdAt", value, constraints, new Guid("381c12d9-9477-4646-aaed-e7a00584832c"), "AggregateMessage.AggregateOccurrerdAt Constraint Exception", inner)
        {
        }
    }
}
