using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class EmailValueObjectConstraintException : StringValueObjectConstraintException
{
    public EmailValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public EmailValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public EmailValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("9ab52afe-120e-47b4-a502-b41b5474f910"), $"{typeof(EmailValueObject).Name} Constraint Exception", innerException, constraints) { }
}