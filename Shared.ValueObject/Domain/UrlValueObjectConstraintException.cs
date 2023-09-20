using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class UrlValueObjectConstraintException : StringValueObjectConstraintException
    {
        public UrlValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public UrlValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public UrlValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("f90ac483-8190-456a-a903-2ac7cd87f010"), "UrlValueObject Constraint Exception", inner)
        {
        }
    }
}
