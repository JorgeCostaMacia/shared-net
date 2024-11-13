using FluentValidation;

namespace Shared.ValueObject.Domain;

public record UrlValueObject : StringValueObject
{
    public UrlValueObject(string value) : base(value) { }

    public static UrlValueObject Create(string value, IValidator<UrlValueObject>? validator = null)
    {
        UrlValueObject ValueObject = new UrlValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }
}