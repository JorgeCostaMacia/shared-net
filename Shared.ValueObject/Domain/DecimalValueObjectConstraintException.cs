using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class DecimalValueObjectConstraintException : IConstraintException
{
    public DecimalValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public DecimalValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public DecimalValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("b834c86a-d716-4063-8fd6-08af398caf37"), "DecimalValueObject Constraint Exception", inner, constraints) { }
}