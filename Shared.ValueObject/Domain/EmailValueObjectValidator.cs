﻿using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class EmailValueObjectValidator : AbstractValidator<EmailValueObject>, Shared.Validator.Domain.IValidator
    {
        public EmailValueObjectValidator(string name = "EmailValueObject")
        {
            Include(new StringValueObjectValidator(name));

            RuleFor(v => v.Value)
                 .NotEmpty()
                 .MinimumLength(1)
                 .EmailAddress()
                 .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<EmailValueObject> context, ValidationResult result) => throw new EmailValueObjectConstraintException(result.Errors);
    }
}
