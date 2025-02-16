using FluentValidation;

namespace Shared.ValueObject.Domain;

public record ByteValueObject : IValueObject
{
    public byte[] Value { get; init; }

    public ByteValueObject(byte[] value)
    {
        Value = value;
    }

    public ByteValueObject Validate(IValidator<ByteValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static ByteValueObject Create(byte[] value) => new ByteValueObject(Convert(value));
    public static ByteValueObject Create() => Create(Array.Empty<byte>());
    public static ByteValueObject Create(string value) => Create(Convert(value));

    protected static byte[] Convert(byte[] value) => value;
    protected static byte[] Convert(string value) => System.Convert.FromBase64String(value.Trim());

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);
    public static implicit operator byte[](ByteValueObject valueObject) => valueObject.Value;
}