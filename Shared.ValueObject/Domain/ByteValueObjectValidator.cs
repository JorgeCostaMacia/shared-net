using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class ByteValueObjectValidator : AbstractValidator<ByteValueObject>, Shared.Validator.Domain.IValidator
    {
        public ByteValueObjectValidator(string name = "ByteValueObject")
        {
            RuleFor(v => v.Value)
                .NotEmpty()
                .WithName(name);

            RuleFor(x => "0x" + BitConverter.ToString(x.Value).Replace("-", ""))
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(2147483647)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<ByteValueObject> context, ValidationResult result) => throw new ByteValueObjectConstraintException(result.Errors);
    }
}
