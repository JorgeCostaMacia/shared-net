using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberValueObjectValidatorTests
{
    private static readonly PageNumberValueObjectValidator _validator = PageNumberValueObjectValidator.Create();

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Positive_Passes(int value)
        => Assert.True(_validator.Validate(PageNumberValueObject.From(value)).IsValid);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void ZeroOrNegative_Fails(int value)
        => Assert.False(_validator.Validate(PageNumberValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<PageNumberValueObjectValidationException>(() => _validator.ValidateAndThrow(PageNumberValueObject.From(0)));
}
