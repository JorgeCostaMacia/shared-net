﻿using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderByValueObjectValidator : AbstractValidator<OrderByValueObject>, Shared.Validator.Domain.IValidator
{
    public OrderByValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty();
    }

    protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result) => throw new OrderByValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}
