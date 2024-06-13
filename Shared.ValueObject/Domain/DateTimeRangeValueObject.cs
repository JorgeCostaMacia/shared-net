﻿using FluentValidation;

namespace Shared.ValueObject.Domain;

public class DateTimeRangeValueObject(DateTimeValueObject valueStart, DateTimeValueObject valueEnd) : IValueObject
{
    public DateTimeValueObject ValueStart { get; init; } = valueStart;
    public DateTimeValueObject ValueEnd { get; init; } = valueEnd;

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

    public override bool Equals(object? obj) => obj is DateTimeRangeValueObject @object && GetType() == @object.GetType() && ValueStart == @object.ValueStart && ValueEnd == @object.ValueEnd;
    public override int GetHashCode() => HashCode.Combine(ValueStart, ValueEnd);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();

    public static bool operator ==(DateTimeRangeValueObject? left, DateTimeRangeValueObject? right) => left?.Equals(right) ?? right?.Equals(left) ?? true;
    public static bool operator !=(DateTimeRangeValueObject? left, DateTimeRangeValueObject? right) => !left?.Equals(right) ?? !right?.Equals(left) ?? false;
}
