using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class EmailValueObjectConstraintException : StringValueObjectConstraintException
    {
        public EmailValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public EmailValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public EmailValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("9ab52afe-120e-47b4-a502-b41b5474f910"), "EmailValueObject Constraint Exception", inner)
        {
        }
    }
}
