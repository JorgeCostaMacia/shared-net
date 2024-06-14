using FluentValidation;

namespace Shared.ValueObject.Domain;

public class OrderTypeValueObject : StringValueObject
{
    public OrderTypeValueObject(string value) : base(value) { }

    public static OrderTypeValueObject Create(string value, IValidator<OrderTypeValueObject>? validator = null)
    {
        OrderTypeValueObject ValueObject = new OrderTypeValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static OrderTypeValueObject Create(IValidator<OrderTypeValueObject>? validator = null) => Create("ASC", validator);

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}