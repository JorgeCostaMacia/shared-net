using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class FloatValueObjectConstraintException : IConstraintException
{
    public FloatValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public FloatValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f40-206d-7df6-8a68-b19c832e3752"), aggregateCode, aggregateOccurredAt, $"{typeof(FloatValueObject).Name} Constraint Exception", innerException, constraints) { }
}