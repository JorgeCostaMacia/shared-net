using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IntRangeValueObjectValidatorTests
{
    private static readonly IntRangeValueObjectValidator Validator = IntRangeValueObjectValidator.Create();

    [Theory]
    [InlineData(1, 10)]
    [InlineData(5, 5)]      // start == end is allowed
    [InlineData(-10, -1)]
    public void StartNotAfterEnd_Passes(int start, int end)
        => Assert.True(Validator.Validate(IntRangeValueObject.From(start, end)).IsValid);

    [Fact]
    public void StartAfterEnd_Fails()
        => Assert.False(Validator.Validate(IntRangeValueObject.From(10, 1)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<IntRangeValueObjectValidationException>(() => Validator.ValidateAndThrow(IntRangeValueObject.From(10, 1)));
}
