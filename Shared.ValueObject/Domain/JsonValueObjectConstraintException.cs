using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class JsonValueObjectConstraintException : StringValueObjectConstraintException
{
    public JsonValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public JsonValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f42-0686-7b9b-bb04-c1cd39ecdf75"), aggregateCode, aggregateOccurredAt, $"{typeof(JsonValueObject).Name} Constraint Exception", innerException, constraints) { }
}