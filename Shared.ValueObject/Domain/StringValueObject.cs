using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using FluentValidation;

namespace Shared.ValueObject.Domain;

public record StringValueObject : IValueObject, IValidatableObject
{
    [MaxLength(100)]
    public string Value { get; init; }

    public StringValueObject(string value)
    {
        Value = value;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Value))
        {
            validationContext.Items["AttemptedValue"] = Value;
            yield return new ValidationResult("El nombre es requerido.", new[] { this.GetType().Name });
        }
    }


    public StringValueObject Validate(IValidator<StringValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static StringValueObject Create(string value) => new StringValueObject(Convert(value));
    public static StringValueObject Create() => Create(string.Empty);
    public static StringValueObject Create(int value) => Create(Convert(value));
    public static StringValueObject Create(float value) => Create(Convert(value));
    public static StringValueObject Create(decimal value) => Create(Convert(value));
    public static StringValueObject Create(bool value) => Create(Convert(value));
    public static StringValueObject Create(DateTime value) => Create(Convert(value));
    public static StringValueObject Create(Guid value) => Create(Convert(value));

    protected static string Convert(string value) => value.Trim();
    protected static string Convert(int value) => value.ToString();
    protected static string Convert(float value) => value.ToString();
    protected static string Convert(decimal value) => value.ToString();
    protected static string Convert(bool value) => value.ToString();
    protected static string Convert(DateTime value) => value.ToString();
    protected static string Convert(Guid value) => value.ToString();

    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value.ToString();
    public static implicit operator string(StringValueObject valueObject) => valueObject.Value;
}