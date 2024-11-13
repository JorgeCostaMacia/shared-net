using FluentValidation;

namespace Shared.ValueObject.Domain;

public record PageNumberRangeValueObject : IValueObject
{
    public PageNumberValueObject ValueStart { get; init; }
    public PageNumberValueObject ValueEnd { get; init; }

    public PageNumberRangeValueObject(PageNumberValueObject valueStart, PageNumberValueObject valueEnd)
    {
        ValueStart = valueStart;
        ValueEnd = valueEnd;
    }

    public static PageNumberRangeValueObject Create(PageNumberValueObject valueStart, PageNumberValueObject valueEnd, IValidator<PageNumberRangeValueObject>? validator = null)
    {
        PageNumberRangeValueObject ValueObject = new PageNumberRangeValueObject(valueStart, valueEnd);
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static PageNumberRangeValueObject Create(IValidator<PageNumberRangeValueObject>? validator = null) => Create(PageNumberValueObject.Create(1), PageNumberValueObject.Create(1), validator);
    public static PageNumberRangeValueObject Create(int valueStart, int valueEnd, IValidator<PageNumberRangeValueObject>? validator = null) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd), validator);
    public static PageNumberRangeValueObject Create(string valueStart, string valueEnd, IValidator<PageNumberRangeValueObject>? validator = null) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd), validator);
    public static PageNumberRangeValueObject Create(float valueStart, float valueEnd, IValidator<PageNumberRangeValueObject>? validator = null) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd), validator);
    public static PageNumberRangeValueObject Create(decimal valueStart, decimal valueEnd, IValidator<PageNumberRangeValueObject>? validator = null) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd), validator);

    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
