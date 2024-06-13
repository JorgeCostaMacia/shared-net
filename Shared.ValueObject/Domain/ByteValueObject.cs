using FluentValidation;

namespace Shared.ValueObject.Domain;

public class ByteValueObject(byte[] value) : IValueObject
{
    public byte[] Value { get; init; } = value;

    public static ByteValueObject Create(byte[] value, IValidator<ByteValueObject>? validator = null)
    {
        ByteValueObject ValueObject = new ByteValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static ByteValueObject Create(IValidator<ByteValueObject>? validator = null) => Create(Array.Empty<byte>(), validator);
    public static ByteValueObject Create(string value, IValidator<ByteValueObject>? validator = null) => Create(Convert(value), validator);

    protected static byte[] Convert(byte[] value) => value;
    protected static byte[] Convert(string value) => System.Convert.FromBase64String(value.Trim());

    public override bool Equals(object? obj) => obj is ByteValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);

    public static bool operator ==(ByteValueObject? left, ByteValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
    public static bool operator !=(ByteValueObject left, ByteValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
}
