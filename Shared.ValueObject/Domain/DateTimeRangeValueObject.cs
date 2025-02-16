using FluentValidation;

namespace Shared.ValueObject.Domain;

public record DateTimeRangeValueObject : IValueObject
{
    public DateTimeValueObject ValueStart { get; init; }
    public DateTimeValueObject ValueEnd { get; init; }

    public DateTimeRangeValueObject(DateTimeValueObject valueStart, DateTimeValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    public DateTimeRangeValueObject Validate(IValidator<DateTimeRangeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static DateTimeRangeValueObject Create(DateTimeValueObject valueStart, DateTimeValueObject valueEnd) => new DateTimeRangeValueObject(valueStart, valueEnd);
    public static DateTimeRangeValueObject Create() => Create(DateTimeValueObject.Create(DateTime.UtcNow.Date), DateTimeValueObject.Create(DateTime.UtcNow.Date));
    public static DateTimeRangeValueObject Create(DateTime valueStart, DateTime valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));
    public static DateTimeRangeValueObject Create(string valueStart, string valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));
    public static DateTimeRangeValueObject Create(int valueStart, int valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));
    public static DateTimeRangeValueObject Create(float valueStart, float valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));
    public static DateTimeRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd));

    public override int GetHashCode() => HashCode.Combine(ValueStart, ValueEnd);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}