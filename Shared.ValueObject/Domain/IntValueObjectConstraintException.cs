using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain
{
    public class IntValueObjectConstraintException : IConstraintException
    {
        public IntValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public IntValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public IntValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("f93ef42d-4b96-4583-a55b-cad3ed54cffb"), "IntValueObject Constraint Exception", inner)
        {
        }
    }
}
