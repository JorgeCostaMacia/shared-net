using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class JsonValueObjectValidatorTests
{
    private static readonly JsonValueObjectValidator _validator = JsonValueObjectValidator.Create();

    [Theory]
    [InlineData("{\"a\":1}")]                  // object
    [InlineData("[1,2,3]")]                    // array
    [InlineData("{\"a\":{\"b\":[1,2]}}")]      // nested
    [InlineData("{\n  \"a\": 1\n}")]           // multi-line (regex used to reject this)
    public void ValidJsonObjectOrArray_Passes(string value)
        => Assert.True(_validator.Validate(JsonValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]                 // empty
    [InlineData("notjson")]          // not JSON
    [InlineData("{\"a\":}")]         // malformed
    [InlineData("123")]              // valid JSON but a scalar, not object/array
    [InlineData("\"a string\"")]     // valid JSON but a scalar
    public void InvalidJson_Fails(string value)
        => Assert.False(_validator.Validate(JsonValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<JsonValueObjectValidationException>(() => _validator.ValidateAndThrow(JsonValueObject.From("notjson")));
}
