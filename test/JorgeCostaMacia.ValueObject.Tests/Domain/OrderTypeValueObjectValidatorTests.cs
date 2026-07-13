using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderTypeValueObjectValidatorTests
{
    private static readonly OrderTypeValueObjectValidator _validator = OrderTypeValueObjectValidator.Create();

    [Theory]
    [InlineData("ASC")]
    [InlineData("DESC")]
    [InlineData("asc")]     // From upper-cases, so lowercase input is valid
    [InlineData("desc")]
    public void ValidOrderType_Passes(string value)
        => Assert.True(_validator.Validate(OrderTypeValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]        // empty
    [InlineData("UP")]      // not a valid direction
    [InlineData("ASCENDING")]
    public void InvalidOrderType_Fails(string value)
        => Assert.False(_validator.Validate(OrderTypeValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<OrderTypeValueObjectValidationException>(() => _validator.ValidateAndThrow(OrderTypeValueObject.From("UP")));
}
