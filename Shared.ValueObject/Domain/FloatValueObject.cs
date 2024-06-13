﻿using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class FloatValueObject(float value) : IValueObject
    {
        public float Value { get; init; } = value;

        public static FloatValueObject Create(float value, IValidator<FloatValueObject>? validator = null)
        {
            FloatValueObject ValueObject = new FloatValueObject(Convert(value));
            validator?.ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static FloatValueObject Create(IValidator<FloatValueObject>? validator = null) => Create(0, validator);
        public static FloatValueObject Create(string value, IValidator<FloatValueObject>? validator = null) => Create(Convert(value), validator);
        public static FloatValueObject Create(int value, IValidator<FloatValueObject>? validator = null) => Create(Convert(value), validator);
        public static FloatValueObject Create(decimal value, IValidator<FloatValueObject>? validator = null) => Create(Convert(value), validator);
        public static FloatValueObject Create(bool value, IValidator<FloatValueObject>? validator = null) => Create(Convert(value), validator);
        public static FloatValueObject Create(DateTime value, IValidator<FloatValueObject>? validator = null) => Create(Convert(value), validator);

        protected static float Convert(float value) => value;
        protected static float Convert(string value) => float.Parse(value.Trim());
        protected static float Convert(int value) => value;
        protected static float Convert(decimal value) => (float)value;
        protected static float Convert(bool value) => value ? 1 : 0;
        protected static float Convert(DateTime value) => (float)new TimeSpan(value.Ticks).TotalSeconds;

        public override bool Equals(object? obj) => obj is FloatValueObject @object && GetType() == @object.GetType() && Value == @object.Value;
        public override int GetHashCode() => HashCode.Combine(Value);
        public override string ToString() => Value.ToString();

        public static bool operator ==(FloatValueObject? left, FloatValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(FloatValueObject? left, FloatValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
        public static bool operator >(FloatValueObject left, FloatValueObject right) => left.Value > right.Value;
        public static bool operator <(FloatValueObject left, FloatValueObject right) => left.Value < right.Value;
        public static bool operator >=(FloatValueObject left, FloatValueObject right) => left.Value >= right.Value;
        public static bool operator <=(FloatValueObject left, FloatValueObject right) => left.Value <= right.Value;
    }
}
