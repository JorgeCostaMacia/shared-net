using FluentValidation;

namespace Shared.ValueObject.Domain;

public record JsonValueObject : StringValueObject
{
    public JsonValueObject(string value) : base(value) { }

    public JsonValueObject Validate(IValidator<JsonValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static JsonValueObject Create(string value) => new JsonValueObject(Convert(value));
    public static JsonValueObject Create() => Create("{}");
}
