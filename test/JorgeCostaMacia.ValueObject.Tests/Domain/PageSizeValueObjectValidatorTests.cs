using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageSizeValueObjectValidatorTests
{
    private static readonly PageSizeValueObjectValidator Validator = new(new IntValueObjectValidator());

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(10000)]
    public void Positive_Passes(int value)
        => Assert.True(Validator.Validate(PageSizeValueObject.Create(value)).IsValid);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ZeroOrNegative_Fails(int value)
        => Assert.False(Validator.Validate(PageSizeValueObject.Create(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<PageSizeValueObjectValidationException>(() => Validator.ValidateAndThrow(PageSizeValueObject.Create(0)));
}
