using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class JsonValueObjectValidatorTests
{
    private static readonly JsonValueObjectValidator _validator = JsonValueObjectValidator.Create();

    // The base takes any JSON document, scalars included: narrowing the kind is a derived Value Object's job.
    [Theory]
    [InlineData("{\"a\":1}")]                  // object
    [InlineData("[1,2,3]")]                    // array
    [InlineData("{\"a\":{\"b\":[1,2]}}")]      // nested
    [InlineData("{\n  \"a\": 1\n}")]           // multi-line (regex used to reject this)
    [InlineData("\"a string\"")]               // string
    [InlineData("123")]                        // number
    [InlineData("true")]                       // boolean
    [InlineData("null")]                       // null literal
    public void AnyJsonDocument_Passes(string value)
        => Assert.True(_validator.Validate(JsonValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]                 // empty
    [InlineData("notjson")]          // not JSON
    [InlineData("{")]                // unterminated
    [InlineData("{\"a\":}")]         // malformed
    [InlineData("{'a':1}")]          // single quotes
    [InlineData("{} trailing")]      // a document followed by more text
    public void InvalidJson_Fails(string value)
        => Assert.False(_validator.Validate(JsonValueObject.From(value)).IsValid);

    // No cascade: an empty value reports both what it is (empty) and what it is not (a JSON).
    [Fact]
    public void Empty_ReportsBothRules()
    {
        ValidationResult result = _validator.Validate(JsonValueObject.From(""));

        Assert.Equal(2, result.Errors.Count);
        Assert.Contains(result.Errors, e => e.ErrorCode == "JsonValidator");
    }

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<JsonValueObjectValidationException>(() => _validator.ValidateAndThrow(JsonValueObject.From("notjson")));

    // The derived Value Object narrows the kind: this is how a field that takes only a JSON object is built on the base.
    [Theory]
    [InlineData("{\"a\":1}", true)]
    [InlineData("[1]", false)]
    [InlineData("5", false)]
    [InlineData("null", false)]
    public void DerivedValidator_NarrowsToAnObject(
        string value,
        bool valid)
        => Assert.Equal(valid, new JsonObjectValidator().Validate(new JsonObject(value)).IsValid);

    public sealed record JsonObject : JsonValueObject
    {
        public JsonObject(string value) : base(value) { }
    }

    public sealed class JsonObjectValidator : AbstractValidator<JsonObject>
    {
        public JsonObjectValidator()
        {
            Include(JsonValueObjectValidator.Create());

            RuleFor(v => v.Value)
                .Must(BeJsonObject)
                .WithErrorCode("JsonObjectValidator")
                .WithMessage("{PropertyName} must be a JSON object");
        }

        private static bool BeJsonObject(string value)
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(value);

                return document.RootElement.ValueKind == JsonValueKind.Object;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
