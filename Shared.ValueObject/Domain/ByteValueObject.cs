using FluentValidation;
using System.Collections;

namespace Shared.ValueObject.Domain
{
    public class ByteValueObject : IValueObject
    {
        public byte[] Value { get; }

        public ByteValueObject(byte[] value)
        {
            Value = value;
        }

        public static ByteValueObject Create(byte[] value, bool validate = true)
        {
            ByteValueObject ValueObject = new ByteValueObject(ToValue(value));
            if (validate) new ByteValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static ByteValueObject Create() => new ByteValueObject(Array.Empty<byte>());
        public static ByteValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);

        protected static byte[] ToValue(byte[] value) => value;
        protected static byte[] ToValue(string value) => Convert.FromBase64String(value.Trim());

        public override bool Equals(object? obj) => obj is ByteValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);

        public static bool operator ==(ByteValueObject? left, ByteValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(ByteValueObject left, ByteValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;

    }
}
