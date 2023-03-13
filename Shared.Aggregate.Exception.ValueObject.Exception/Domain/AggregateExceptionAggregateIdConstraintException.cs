using Shared.ValueObject.Exception.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Domain
{
    public class AggregateExceptionAggregateIdConstraintException : UuidValueObjectConstraintException
    {
        public AggregateExceptionAggregateIdConstraintException(Guid value, List<string> constraints) : base("AggregateId", value, constraints, new Guid("4d44e36f-346b-4de1-92b6-1e95b68261d5"), "AggregateException.AggregateId Constraint Exception")
        {
        }

        public AggregateExceptionAggregateIdConstraintException(Guid value, List<string> constraints, System.Exception inner) : base("AggregateId", value, constraints, new Guid("4d44e36f-346b-4de1-92b6-1e95b68261d5"), "AggregateException.AggregateId Constraint Exception", inner)
        {
        }
    }
}
