using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IntRangeValueObjectValidatorTests
{
    private static readonly IntRangeValueObjectValidator _validator = IntRangeValueObjectValidator.Create();

    [Theory]
    [InlineData(1, 10)]
    [InlineData(5, 5)]      // start == end is allowed
    [InlineData(-10, -1)]
    public void StartNotAfterEnd_Passes(int start, int end)
        => Assert.True(_validator.Validate(IntRangeValueObject.From(start, end)).IsValid);

    [Fact]
    public void StartAfterEnd_Fails()
        => Assert.False(_validator.Validate(IntRangeValueObject.From(10, 1)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<IntRangeValueObjectValidationException>(() => _validator.ValidateAndThrow(IntRangeValueObject.From(10, 1)));
}
