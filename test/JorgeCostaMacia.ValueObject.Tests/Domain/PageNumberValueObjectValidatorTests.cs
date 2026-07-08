using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberValueObjectValidatorTests
{
    private static readonly PageNumberValueObjectValidator Validator = new(new IntValueObjectValidator());

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Positive_Passes(int value)
        => Assert.True(Validator.Validate(PageNumberValueObject.Create(value)).IsValid);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void ZeroOrNegative_Fails(int value)
        => Assert.False(Validator.Validate(PageNumberValueObject.Create(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<PageNumberValueObjectValidationException>(() => Validator.ValidateAndThrow(PageNumberValueObject.Create(0)));
}
