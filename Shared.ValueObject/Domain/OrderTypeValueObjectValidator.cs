﻿using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderTypeValueObjectValidator : AbstractValidator<OrderTypeValueObject>, Shared.Validator.Domain.IValidator
{
    public OrderTypeValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .Must(v2 => v2 == "ASC" || v2 == "DESC");
    }

    protected override void RaiseValidationException(ValidationContext<OrderTypeValueObject> context, ValidationResult result) => throw new OrderTypeValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}