using FluentValidation;

namespace Shared.ValueObject.Domain;

public record OrderTypeValueObject : StringValueObject
{
    public OrderTypeValueObject(string value) : base(value) { }

    public OrderTypeValueObject Validate(IValidator<OrderTypeValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static OrderTypeValueObject Create(string value) => new OrderTypeValueObject(Convert(value));
    public static OrderTypeValueObject Create() => Create("ASC");

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}