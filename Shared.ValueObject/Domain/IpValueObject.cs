using FluentValidation;

namespace Shared.ValueObject.Domain;

public record IpValueObject : StringValueObject
{
    public IpValueObject(string value) : base(value) { }

    public IpValueObject Validate(IValidator<IpValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static IpValueObject Create(string value) => new IpValueObject(Convert(value));
    public static IpValueObject Create() => Create("0.0.0.0");
}