using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class FloatValueObjectConstraintException : IConstraintException
{
    public FloatValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public FloatValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public FloatValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception", inner, constraints) { }
}