using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class PageNumberValueObjectConstraintException : IntValueObjectConstraintException
    {
        public PageNumberValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public PageNumberValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public PageNumberValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("df74e0e0-e20c-4135-b583-690f15b8ac1d"), "PageNumberValueObject Constraint Exception", inner)
        {
        }
    }
}
