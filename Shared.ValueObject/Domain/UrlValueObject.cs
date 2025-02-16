using FluentValidation;

namespace Shared.ValueObject.Domain;

public record UrlValueObject : StringValueObject
{
    public UrlValueObject(string value) : base(value) { }

    public UrlValueObject Validate(IValidator<UrlValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static new UrlValueObject Create(string value) => new UrlValueObject(Convert(value));
}