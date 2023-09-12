using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObjectConstraintException : RangeValueObjectConstraintException
    {
        public IntRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public IntRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public IntRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("67ec51e3-0abd-40ec-a356-21371cbc1f47"), "IntRangeValueObject Constraint Exception", inner)
        {
        }
    }
}
