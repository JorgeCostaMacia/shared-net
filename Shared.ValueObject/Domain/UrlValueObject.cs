using FluentValidation;

namespace Shared.ValueObject.Domain;

public class UrlValueObject(string value) : StringValueObject(value)
{
    public static UrlValueObject Create(string value, IValidator<UrlValueObject>? validator = null)
    {
        UrlValueObject ValueObject = new UrlValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }
}