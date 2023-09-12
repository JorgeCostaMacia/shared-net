using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class JsonValueObjectConstraintException : StringValueObjectConstraintException
    {
        public JsonValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public JsonValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public JsonValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("64c55eae-817a-4591-84cb-24dc2b33e9df"), "JsonValueObject Constraint Exception", inner)
        {
        }
    }
}
