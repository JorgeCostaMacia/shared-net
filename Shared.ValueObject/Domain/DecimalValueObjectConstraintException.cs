﻿using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class DecimalValueObjectConstraintException : IConstraintException
{
    public DecimalValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public DecimalValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public DecimalValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("b834c86a-d716-4063-8fd6-08af398caf37"), $"{typeof(DecimalValueObject).Name}  Constraint Exception", innerException, constraints) { }
}