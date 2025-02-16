using FluentValidation;

namespace Shared.ValueObject.Domain;

public record OrderByValueObject : StringValueObject
{
    public OrderByValueObject(string value) : base(value) { }

    public OrderByValueObject Validate(IValidator<OrderByValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static OrderByValueObject Create(string value) => new OrderByValueObject(Convert(value));

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}