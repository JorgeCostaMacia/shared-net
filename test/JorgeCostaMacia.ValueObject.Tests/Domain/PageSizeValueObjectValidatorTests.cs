using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageSizeValueObjectValidatorTests
{
    private static readonly PageSizeValueObjectValidator _validator = PageSizeValueObjectValidator.Create();

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(10000)]
    public void Positive_Passes(int value)
        => Assert.True(_validator.Validate(PageSizeValueObject.From(value)).IsValid);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ZeroOrNegative_Fails(int value)
        => Assert.False(_validator.Validate(PageSizeValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<PageSizeValueObjectValidationException>(() => _validator.ValidateAndThrow(PageSizeValueObject.From(0)));
}
