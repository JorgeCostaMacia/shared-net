
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Domain
{
    public class AggregateExceptionAggregateCodeConstraintException : IntValueObjectConstraintException
    {
        public AggregateExceptionAggregateCodeConstraintException(AggregateExceptionAggregateCode value, List<string> constraints) : base("AggregateCode", value, constraints, new Guid("bc8acd45-ea3d-43e7-853d-b65cd418ae6b"), "AggregateException.AggregateCode Constraint Exception")
        {
        }

        public AggregateExceptionAggregateCodeConstraintException(AggregateExceptionAggregateCode value, List<string> constraints, System.Exception inner) : base("AggregateCode", value, constraints, new Guid("bc8acd45-ea3d-43e7-853d-b65cd418ae6b"), "AggregateException.AggregateCode Constraint Exception", inner)
        {
        }
    }
}