using FluentValidation.Results;
using Shared.Exception.Domain;
using System.Collections.Immutable;

namespace Shared.ValueObject.Domain
{
    public class BoolValueObjectConstraintException : IConstraintException
    {
        public BoolValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public BoolValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public BoolValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), "BoolValueObject Constraint Exception", inner)
        {
        }
    }
}
