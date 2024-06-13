using FluentValidation;

namespace Shared.ValueObject.Domain;

public class PageNumberValueObject(int value) : IntValueObject(value)
{
    public static PageNumberValueObject Create(int value, IValidator<PageNumberValueObject>? validator = null)
    {
        PageNumberValueObject ValueObject = new PageNumberValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static PageNumberValueObject Create(IValidator<PageNumberValueObject>? validator = null) => Create(1, validator);
    public static PageNumberValueObject Create(string value, IValidator<PageNumberValueObject>? validator = null) => Create(Convert(value), validator);
    public static PageNumberValueObject Create(float value, IValidator<PageNumberValueObject>? validator = null) => Create(Convert(value), validator);
    public static PageNumberValueObject Create(decimal value, IValidator<PageNumberValueObject>? validator = null) => Create(Convert(value), validator);
}