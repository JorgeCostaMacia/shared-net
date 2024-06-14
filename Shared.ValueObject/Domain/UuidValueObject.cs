using FluentValidation;

namespace Shared.ValueObject.Domain;

public class UuidValueObject : IValueObject
{
    public Guid Value { get; init; }

    public UuidValueObject(Guid value)
    {
        Value = value;
    }

    public static UuidValueObject Create(Guid value, IValidator<UuidValueObject>? validator = null)
    {
        UuidValueObject ValueObject = new UuidValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static UuidValueObject Create(IValidator<UuidValueObject>? validator = null) => Create(Guid.NewGuid(), validator);
    public static UuidValueObject Create(string value, IValidator<UuidValueObject>? validator = null) => Create(Convert(value), validator);

    protected static Guid Convert(Guid value) => value;
    protected static Guid Convert(string value) => new Guid(value.Trim());

    public override bool Equals(object? obj) => obj is UuidValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();

    public static bool operator ==(UuidValueObject? left, UuidValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
    public static bool operator !=(UuidValueObject left, UuidValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
}
