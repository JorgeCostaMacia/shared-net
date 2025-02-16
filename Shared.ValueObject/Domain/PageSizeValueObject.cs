using FluentValidation;

namespace Shared.ValueObject.Domain;

public record PageSizeValueObject : IntValueObject
{
    public PageSizeValueObject(int value) : base(value) { }

    public PageSizeValueObject Validate(IValidator<PageSizeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static new PageSizeValueObject Create(int value) => new PageSizeValueObject(Convert(value));
    public static new PageSizeValueObject Create() => Create(10000);
    public static new PageSizeValueObject Create(string value) => Create(Convert(value));
    public static new PageSizeValueObject Create(float value) => Create(Convert(value));
    public static new PageSizeValueObject Create(decimal value) => Create(Convert(value));
}