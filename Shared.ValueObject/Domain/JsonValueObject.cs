using FluentValidation;

namespace Shared.ValueObject.Domain;

public record JsonValueObject : StringValueObject
{
    public JsonValueObject(string value) : base(value) { }

    public static JsonValueObject Create(string value, IValidator<JsonValueObject>? validator = null)
    {
        JsonValueObject ValueObject = new JsonValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    public static JsonValueObject Create(IValidator<JsonValueObject>? validator = null) => Create("{}", validator);
}
