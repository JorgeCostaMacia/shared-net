using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class OrderByValueObjectValidatorTests
{
    private static readonly OrderByValueObjectValidator Validator = new(new StringValueObjectValidator());

    [Theory]
    [InlineData("name")]
    [InlineData("created_at")]
    public void NonEmpty_Passes(string value)
        => Assert.True(Validator.Validate(OrderByValueObject.Create(value)).IsValid);

    [Fact]
    public void Empty_Fails()
        => Assert.False(Validator.Validate(OrderByValueObject.Create("")).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<OrderByValueObjectValidationException>(() => Validator.ValidateAndThrow(OrderByValueObject.Create("")));
}
