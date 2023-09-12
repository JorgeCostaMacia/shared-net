using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class OrderTypeValueObjectConstraintException : StringValueObjectConstraintException
    {
        public OrderTypeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public OrderTypeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public OrderTypeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), "OrderTypeValueObject Constraint Exception", inner)
        {
        }
    }
}
