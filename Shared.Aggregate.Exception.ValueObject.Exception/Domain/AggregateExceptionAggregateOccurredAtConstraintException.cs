using Shared.ValueObject.Exception.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Domain
{
    public class AggregateExceptionAggregateOccurredAtConstraintException : DateTimeValueObjectConstraintException
    {
        public AggregateExceptionAggregateOccurredAtConstraintException(DateTime value, List<string> constraints) : base("AggregateOccurrerdAt", value, constraints, new Guid("c655a238-b895-48c2-8fec-241371277dfe"), "AggregateException.AggregateOccurrerdAt Constraint Exception")
        {
        }

        public AggregateExceptionAggregateOccurredAtConstraintException(DateTime value, List<string> constraints, System.Exception inner) : base("AggregateOccurrerdAt", value, constraints, new Guid("c655a238-b895-48c2-8fec-241371277dfe"), "AggregateException.AggregateOccurrerdAt Constraint Exception", inner)
        {
        }
    }
}
