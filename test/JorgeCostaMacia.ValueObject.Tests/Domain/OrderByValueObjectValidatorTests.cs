using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderByValueObjectValidatorTests
{
    private static readonly OrderByValueObjectValidator _validator = OrderByValueObjectValidator.Create();

    [Theory]
    [InlineData("name")]
    [InlineData("created_at")]
    public void NonEmpty_Passes(string value)
        => Assert.True(_validator.Validate(OrderByValueObject.From(value)).IsValid);

    [Fact]
    public void Empty_Fails()
        => Assert.False(_validator.Validate(OrderByValueObject.From("")).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<OrderByValueObjectValidationException>(() => _validator.ValidateAndThrow(OrderByValueObject.From("")));
}
