using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class GroupByValueObjectConstraintException : StringValueObjectConstraintException
    {
        public GroupByValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public GroupByValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public GroupByValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("1e2ffc5a-a2fa-4be2-8837-4fefe0520ef5"), "GroupByValueObject Constraint Exception", inner)
        {
        }
    }
}
