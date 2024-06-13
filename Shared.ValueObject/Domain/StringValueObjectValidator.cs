﻿using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class StringValueObjectValidator : AbstractValidator<StringValueObject>, Shared.Validator.Domain.IValidator
{
    public StringValueObjectValidator(string name = "StringValueObject")
    {
    }

    protected override void RaiseValidationException(ValidationContext<StringValueObject> context, ValidationResult result) => throw new StringValueObjectConstraintException(result.Errors);
}
