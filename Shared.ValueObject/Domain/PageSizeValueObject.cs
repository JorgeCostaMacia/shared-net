using FluentValidation;

namespace Shared.ValueObject.Domain;

public record PageSizeValueObject : IntValueObject
{
    public PageSizeValueObject(int value) : base(value) { }

    public static PageSizeValueObject Create(int value, IValidator<PageSizeValueObject>? validator = null)
    {
        PageSizeValueObject ValueObject = new PageSizeValueObject(Convert(value));
        if (validator != null) validator.ValidateAndThrowAsync(ValueObject);

        return ValueObject;
    }

    public static PageSizeValueObject Create(IValidator<PageSizeValueObject>? validator = null) => Create(10000, validator);
    public static PageSizeValueObject Create(string value, IValidator<PageSizeValueObject>? validator = null) => Create(Convert(value), validator);
    public static PageSizeValueObject Create(float value, IValidator<PageSizeValueObject>? validator = null) => Create(Convert(value), validator);
    public static PageSizeValueObject Create(decimal value, IValidator<PageSizeValueObject>? validator = null) => Create(Convert(value), validator);
}