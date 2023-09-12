using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObjectConstraintException : RangeValueObjectConstraintException
    {
        public FloatRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public FloatRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public FloatRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), "FloatRangeValueObject Constraint Exception", inner)
        {
        }
    }
}
