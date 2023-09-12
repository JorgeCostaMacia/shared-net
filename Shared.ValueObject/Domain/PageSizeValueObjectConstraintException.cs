using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class PageSizeValueObjectConstraintException : IntValueObjectConstraintException
    {
        public PageSizeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public PageSizeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public PageSizeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("b2b4f1e1-caae-4813-9ebd-ddc63c527cae"), "PageSizeValueObject Constraint Exception", inner)
        {
        }
    }
}
