using FluentValidation;

namespace Shared.ValueObject.Domain;

public record DateTimeValueObject : IValueObject
{
    public DateTime Value { get; init; }

    public DateTimeValueObject(DateTime value)
    {
        Value = value;
    }

    public DateTimeValueObject Validate(IValidator<DateTimeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static DateTimeValueObject Create(DateTime value) => new DateTimeValueObject(Convert(value));
    public static DateTimeValueObject Create() => Create(DateTime.UtcNow);
    public static DateTimeValueObject Create(DateTime valueDate, DateTime valueTime) => Create(Convert(valueDate, valueTime));
    public static DateTimeValueObject Create(DateOnly valueDate, TimeOnly valueTime) => Create(Convert(valueDate, valueTime));
    public static DateTimeValueObject Create(string value) => Create(Convert(value));
    public static DateTimeValueObject Create(int value) => Create(Convert(value));
    public static DateTimeValueObject Create(float value) => Create(Convert(value));
    public static DateTimeValueObject Create(decimal value) => Create(Convert(value));

    protected static DateTime Convert(DateTime value) => value.ToUniversalTime();
    protected static DateTime Convert(DateTime valueDate, DateTime valueTime) => valueDate.Date.AddHours(valueDate.Hour).AddMinutes(valueTime.Minute).AddSeconds(valueTime.Second).AddMilliseconds(valueTime.Millisecond)
#if !NET6_0
        .AddMicroseconds(valueTime.Microsecond)
#endif 
        ;
    protected static DateTime Convert(DateOnly valueDate, TimeOnly valueTime) => valueDate.ToDateTime(valueTime, DateTimeKind.Utc);
    protected static DateTime Convert(string value) => DateTime.Parse(value.Trim());
    protected static DateTime Convert(string valueDate, string valueTime) => DateOnly.FromDateTime(DateTime.Parse(valueDate.Trim())).ToDateTime(TimeOnly.FromDateTime(DateTime.Parse(valueTime.Trim())));
    protected static DateTime Convert(int value) => new DateTime(value, DateTimeKind.Utc);
    protected static DateTime Convert(float value) => new DateTime((int)value, DateTimeKind.Utc);
    protected static DateTime Convert(decimal value) => new DateTime((int)value, DateTimeKind.Utc);

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
    public static implicit operator DateTime(DateTimeValueObject valueObject) => valueObject.Value;
}