using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class UuidValueObjectConstraintException : IConstraintException
{
    public UuidValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public UuidValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public UuidValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), "UuidValueObject Constraint Exception", inner, constraints) { }
}