using Shared.ValueObject.Exception.Domain;

namespace Shared.Aggregate.Message.ValueObject.Domain
{
    public class AggregateMessageAggregateIdConstraintException : UuidValueObjectConstraintException
    {
        public AggregateMessageAggregateIdConstraintException(AggregateMessageAggregateId value, List<string> constraints) : base("AggregateId", value, constraints, new Guid("d0e9390d-b354-47ee-9aab-8dbb494944cb"), "AggregateMessage.AggregateId Constraint Exception")
        {
        }

        public AggregateMessageAggregateIdConstraintException(AggregateMessageAggregateId value, List<string> constraints, System.Exception inner) : base("AggregateId", value, constraints, new Guid("d0e9390d-b354-47ee-9aab-8dbb494944cb"), "AggregateMessage.AggregateId Constraint Exception", inner)
        {
        }
    }
}
