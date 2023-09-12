using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain
{
    public class FloatValueObjectConstraintException : IConstraintException
    {
        public FloatValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public FloatValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public FloatValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception", inner)
        {
        }
    }
}
