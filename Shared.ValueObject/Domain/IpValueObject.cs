using FluentValidation;

namespace Shared.ValueObject.Domain;

public class IpValueObject(string value) : StringValueObject(value)
{
    public static IpValueObject Create(string value, IValidator<IpValueObject>? validator = null)
    {
        IpValueObject ValueObject = new IpValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static IpValueObject Create(IValidator<IpValueObject>? validator = null) => Create("0.0.0.0", validator);
}
