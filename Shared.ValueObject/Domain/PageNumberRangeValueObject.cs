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

    public PageNumberRangeValueObject Validate(IValidator<PageNumberRangeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static PageNumberRangeValueObject Create(PageNumberValueObject valueStart, PageNumberValueObject valueEnd) => new PageNumberRangeValueObject(valueStart, valueEnd);
    public static PageNumberRangeValueObject Create() => Create(PageNumberValueObject.Create(1), PageNumberValueObject.Create(1));
    public static PageNumberRangeValueObject Create(int valueStart, int valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));
    public static PageNumberRangeValueObject Create(string valueStart, string valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));
    public static PageNumberRangeValueObject Create(float valueStart, float valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));
    public static PageNumberRangeValueObject Create(decimal valueStart, decimal valueEnd) => Create(PageNumberValueObject.Create(valueStart), PageNumberValueObject.Create(valueEnd));

    public override int GetHashCode() => HashCode.Combine(ValueStart.Value, ValueEnd.Value);
    public override string ToString() => ValueStart.ToString() + " - " + ValueEnd.ToString();
}
