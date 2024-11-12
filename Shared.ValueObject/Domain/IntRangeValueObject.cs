﻿using FluentValidation;

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

    public static IntRangeValueObject Create(IntValueObject valueStart, IntValueObject valueEnd, IValidator<IntRangeValueObject>? validator = null)
    {
        IntRangeValueObject ValueObject = new IntRangeValueObject(valueStart, valueEnd);
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static IntRangeValueObject Create(IValidator<IntRangeValueObject>? validator = null) => Create(IntValueObject.Create(0), IntValueObject.Create(0), validator);
    public static IntRangeValueObject Create(int valueStart, int valueEnd, IValidator<IntRangeValueObject>? validator = null) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd), validator);
    public static IntRangeValueObject Create(string valueStart, string valueEnd, IValidator<IntRangeValueObject>? validator = null) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd), validator);
    public static IntRangeValueObject Create(float valueStart, float valueEnd, IValidator<IntRangeValueObject>? validator = null) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd), validator);
    public static IntRangeValueObject Create(decimal valueStart, decimal valueEnd, IValidator<IntRangeValueObject>? validator = null) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd), validator);
    public static IntRangeValueObject Create(DateTime valueStart, DateTime valueEnd, IValidator<IntRangeValueObject>? validator = null) => Create(IntValueObject.Create(valueStart), IntValueObject.Create(valueEnd), validator);

    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
