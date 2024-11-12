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

    public static DateTimeRangeValueObject Create(DateTimeValueObject valueStart, DateTimeValueObject valueEnd, IValidator<DateTimeRangeValueObject>? validator = null)
    {
        DateTimeRangeValueObject ValueObject = new DateTimeRangeValueObject(valueStart, valueEnd);
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static DateTimeRangeValueObject Create(IValidator<DateTimeRangeValueObject>? validator = null) => Create(DateTimeValueObject.Create(DateTime.UtcNow.Date), DateTimeValueObject.Create(DateTime.UtcNow.Date), validator);
    public static DateTimeRangeValueObject Create(DateTime valueStart, DateTime valueEnd, IValidator<DateTimeRangeValueObject>? validator = null) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd), validator);
    public static DateTimeRangeValueObject Create(string valueStart, string valueEnd, IValidator<DateTimeRangeValueObject>? validator = null) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd), validator);
    public static DateTimeRangeValueObject Create(int valueStart, int valueEnd, IValidator<DateTimeRangeValueObject>? validator = null) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd), validator);
    public static DateTimeRangeValueObject Create(float valueStart, float valueEnd, IValidator<DateTimeRangeValueObject>? validator = null) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd), validator);
    public static DateTimeRangeValueObject Create(decimal valueStart, decimal valueEnd, IValidator<DateTimeRangeValueObject>? validator = null) => Create(DateTimeValueObject.Create(valueStart), DateTimeValueObject.Create(valueEnd), validator);

    public override int GetHashCode() => HashCode.Combine(ValueStart, ValueEnd);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}