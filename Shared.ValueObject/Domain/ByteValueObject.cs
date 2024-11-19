using FluentValidation;

namespace Shared.ValueObject.Domain;

public record ByteValueObject : IValueObject
{
    public byte[] Value { get; init; }

    public ByteValueObject(byte[] value)
    {
        Value = value;
    }

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

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);
    public static implicit operator byte[](ByteValueObject valueObject) => valueObject.Value;
}