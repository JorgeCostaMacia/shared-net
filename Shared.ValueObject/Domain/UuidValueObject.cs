using FluentValidation;

namespace Shared.ValueObject.Domain;

public record UuidValueObject : IValueObject
{
    public Guid Value { get; init; }

    public UuidValueObject(Guid value)
    {
        Value = value;
    }

    public UuidValueObject Validate(IValidator<UuidValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static UuidValueObject Create(Guid value) => new UuidValueObject(Convert(value));
    public static UuidValueObject Create() => Create(
#if NET9_0
    Guid.CreateVersion7()
#else
    Guid.NewGuid()
#endif
    );
    public static UuidValueObject Create(string value) => Create(Convert(value));

    protected static Guid Convert(Guid value) => value;
    protected static Guid Convert(string value) => new Guid(value.Trim());

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();

    public static implicit operator Guid(UuidValueObject valueObject) => valueObject.Value;
}