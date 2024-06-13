using FluentValidation;

namespace Shared.ValueObject.Domain;

public class PageSizeValueObject(int value) : IntValueObject(value)
{
    public static PageSizeValueObject Create(int value, IValidator<PageSizeValueObject>? validator = null)
    {
        PageSizeValueObject ValueObject = new PageSizeValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static PageSizeValueObject Create(IValidator<PageSizeValueObject>? validator = null) => Create(10000, validator);
    public static PageSizeValueObject Create(string value, IValidator<PageSizeValueObject>? validator = null) => Create(Convert(value), validator);
    public static PageSizeValueObject Create(float value, IValidator<PageSizeValueObject>? validator = null) => Create(Convert(value), validator);
    public static PageSizeValueObject Create(decimal value, IValidator<PageSizeValueObject>? validator = null) => Create(Convert(value), validator);
}