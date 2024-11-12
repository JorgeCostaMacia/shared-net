using FluentValidation;

namespace Shared.ValueObject.Domain;

public record UuidValueObject : IValueObject
{
    public Guid Value { get; init; }

    public UuidValueObject(Guid value)
    {
        Value = value;
    }

    public static UuidValueObject Create(Guid value, IValidator<UuidValueObject>? validator = null)
    {
        UuidValueObject ValueObject = new UuidValueObject(Convert(value));
        if (validator != null) validator.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static UuidValueObject Create(IValidator<UuidValueObject>? validator = null) => Create(Guid.NewGuid(), validator);
    public static UuidValueObject Create(string value, IValidator<UuidValueObject>? validator = null) => Create(Convert(value), validator);

    protected static Guid Convert(Guid value) => value;
    protected static Guid Convert(string value) => new Guid(value.Trim());

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
}

