using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class StringValueObjectConstraintException : IConstraintException
{
    public StringValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public StringValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public StringValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("1a66f23c-17f7-40bc-b92a-de79f9b095c3"), "StringValueObject Constraint Exception", inner, constraints) { }
}