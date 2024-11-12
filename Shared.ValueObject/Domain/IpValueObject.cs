using FluentValidation;

namespace Shared.ValueObject.Domain;

public record IpValueObject : StringValueObject
{
    public IpValueObject(string value) : base(value) { }

    public static IpValueObject Create(string value, IValidator<IpValueObject>? validator = null)
    {
        IpValueObject ValueObject = new IpValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static IpValueObject Create(IValidator<IpValueObject>? validator = null) => Create("0.0.0.0", validator);
}