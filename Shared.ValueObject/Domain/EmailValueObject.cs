using FluentValidation;

namespace Shared.ValueObject.Domain;

public record EmailValueObject : StringValueObject
{
    public EmailValueObject(string value) : base(value) { }

    public EmailValueObject Validate(IValidator<EmailValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static EmailValueObject Create(string value) => new EmailValueObject(Convert(value));
}