using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObject : IValueObject
    {
        public IntValueObject ValueStart { get; }
        public IntValueObject ValueEnd { get; }

        public IntRangeValueObject(IntValueObject valueStart, IntValueObject valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static IntRangeValueObject Create(IntValueObject valueStart, IntValueObject valueEnd, bool validate = true)
        {
            IntRangeValueObject ValueObject = new IntRangeValueObject(valueStart, valueEnd);
            if (validate) new IntRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static IntRangeValueObject Create() => new IntRangeValueObject(IntValueObject.Create(0), IntValueObject.Create(0));
        public static IntRangeValueObject Create(int valueStart, int valueEnd, bool validate = true) => Create(IntValueObject.Create(valueStart, false), IntValueObject.Create(valueEnd, false), validate);

        public override bool Equals(object? obj) => obj is IntRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
        public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
        public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

        public static bool operator ==(IntRangeValueObject? left, IntRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(IntRangeValueObject? left, IntRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
