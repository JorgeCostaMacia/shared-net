using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class UrlValueObjectConstraintException : StringValueObjectConstraintException
{
    public UrlValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public UrlValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public UrlValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("f90ac483-8190-456a-a903-2ac7cd87f010"), $"{typeof(UrlValueObject).Name} Constraint Exception", innerException, constraints) { }
}