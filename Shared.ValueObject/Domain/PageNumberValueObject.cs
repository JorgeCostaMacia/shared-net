using FluentValidation;

namespace Shared.ValueObject.Domain;

public record PageNumberValueObject : IntValueObject
{
    public PageNumberValueObject(int value) : base(value) { }

    public PageNumberValueObject Validate(IValidator<PageNumberValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static new PageNumberValueObject Create(int value) => new PageNumberValueObject(Convert(value));
    public static new PageNumberValueObject Create() => Create(1);
    public static new PageNumberValueObject Create(string value) => Create(Convert(value));
    public static new PageNumberValueObject Create(float value) => Create(Convert(value));
    public static new PageNumberValueObject Create(decimal value) => Create(Convert(value));
}