using FluentValidation;

namespace Shared.ValueObject.Domain;

public record IntRangeValueObject : IValueObject
{
    public IntValueObject ValueStart { get; init; }
    public IntValueObject ValueEnd { get; init; }

    public IntRangeValueObject(IntValueObject valueStart, IntValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    public IntRangeValueObject Validate(IValidator<IntRangeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static IntRangeValueObject Create(IntValueObject valueStart, IntValueObject valueEnd) => new IntRangeValueObject(valueStart, valueEnd);
    public static IntRangeValueObject Create() => Create(IntValueObject.Create(0), IntValueObject.Create(0));
    public static IntRangeValueObject Create(int valueStart, int valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));
    public static IntRangeValueObject Create(string valueStart, string valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));
    public static IntRangeValueObject Create(float valueStart, float valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));
    public static IntRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));
    public static IntRangeValueObject Create(DateTime valueStart, DateTime valueEnd) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd));

    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
