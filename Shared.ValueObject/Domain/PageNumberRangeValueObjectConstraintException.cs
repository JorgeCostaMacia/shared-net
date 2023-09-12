using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObjectConstraintException : IntRangeValueObjectConstraintException
    {
        public PageNumberRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public PageNumberRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public PageNumberRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("c5392e0b-afd2-4bfb-8447-1d5693ffce51"), "PageNumberRangeValueObject Constraint Exception", inner)
        {
        }
    }
}
