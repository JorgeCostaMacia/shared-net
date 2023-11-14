﻿using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class ByteValueObject : IValueObject
    {
        public byte[] Value { get; init; }

        public ByteValueObject(byte[] value)
        {
            Value = value;
        }

        public static ByteValueObject From(byte[] value, bool validate = true)
        {
            ByteValueObject ValueObject = new ByteValueObject(Convert(value));
            if (validate) new ByteValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static ByteValueObject From() => From(Array.Empty<byte>());
        public static ByteValueObject From(string value, bool validate = true) => From(Convert(value), validate);

        protected static byte[] Convert(byte[] value) => value;
        protected static byte[] Convert(string value) => System.Convert.FromBase64String(value.Trim());

        public override bool Equals(object? obj) => obj is ByteValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => System.Text.Encoding.UTF8.GetString(Value);

        public static bool operator ==(ByteValueObject? left, ByteValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(ByteValueObject left, ByteValueObject right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;

    }
}
