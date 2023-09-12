using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain
{
    public class DecimalValueObjectConstraintException : IConstraintException
    {
        public DecimalValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public DecimalValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public DecimalValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("b834c86a-d716-4063-8fd6-08af398caf37"), "DecimalValueObject Constraint Exception", inner)
        {
        }
    }
}
