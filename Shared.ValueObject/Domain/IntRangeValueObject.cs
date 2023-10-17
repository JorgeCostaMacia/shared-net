using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObject : IValueObject
    {
        public IntValueObject ValueStart { get; init; }
        public IntValueObject ValueEnd { get; init; }

        public IntRangeValueObject(IntValueObject valueStart, IntValueObject valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public static IntRangeValueObject From(int valueStart, int valueEnd, bool validate = true)
        {
            IntRangeValueObject ValueObject = new IntRangeValueObject(IntValueObject.From(valueStart, false), IntValueObject.From(valueEnd, false));
            if (validate) new IntRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static IntRangeValueObject From() => From(0, 0);
        public static IntRangeValueObject From(IntValueObject valueStart, IntValueObject valueEnd, bool validate = true) => From(valueStart.Value, valueEnd.Value, validate);
        public static IntRangeValueObject From(string valueStart, string valueEnd, bool validate = true) => From(IntValueObject.From(valueStart, false), IntValueObject.From(valueEnd, false), validate);
        public static IntRangeValueObject From(float valueStart, float valueEnd, bool validate = true) => From(IntValueObject.From(valueStart, false), IntValueObject.From(valueEnd, false), validate);
        public static IntRangeValueObject From(decimal valueStart, decimal valueEnd, bool validate = true) => From(IntValueObject.From(valueStart, false), IntValueObject.From(valueEnd, false), validate);
        public static IntRangeValueObject From(DateTime valueStart, DateTime valueEnd, bool validate = true) => From(IntValueObject.From(valueStart, false), IntValueObject.From(valueEnd, false), validate);

        public static IntRangeValueObject? FromOrDefault(int? valueStart, int? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((int)valueStart, (int)valueEnd, validate) : From();
        public static IntRangeValueObject? FromOrDefault(IntValueObject? valueStart, IntValueObject? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From(valueStart, valueEnd, validate) : From();
        public static IntRangeValueObject? FromOrDefault(string? valueStart, string? valueEnd, bool validate = true) => valueStart != null && valueEnd != null && valueStart != "" && valueEnd != "" ? From(valueStart, valueEnd, validate) : From();
        public static IntRangeValueObject? FromOrDefault(float? valueStart, float? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((float)valueStart, (float)valueEnd, validate) : From();
        public static IntRangeValueObject? FromOrDefault(decimal? valueStart, decimal? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((decimal)valueStart, (decimal)valueEnd, validate) : From();
        public static IntRangeValueObject? FromOrDefault(DateTime? valueStart, DateTime? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((DateTime)valueStart, (DateTime)valueEnd, validate) : From();

        public override bool Equals(object? obj) => obj is IntRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
        public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
        public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

        public static bool operator ==(IntRangeValueObject? left, IntRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
        public static bool operator !=(IntRangeValueObject? left, IntRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
    }
}
