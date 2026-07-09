using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class FloatRangeValueObjectValidatorTests
{
    private static readonly FloatRangeValueObjectValidator Validator = FloatRangeValueObjectValidator.Create();

    [Theory]
    [InlineData(1f, 10f)]
    [InlineData(2.5f, 2.5f)]   // start == end is allowed
    public void StartNotAfterEnd_Passes(float start, float end)
        => Assert.True(Validator.Validate(FloatRangeValueObject.From(start, end)).IsValid);

    [Fact]
    public void StartAfterEnd_Fails()
        => Assert.False(Validator.Validate(FloatRangeValueObject.From(10f, 1f)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<FloatRangeValueObjectValidationException>(() => Validator.ValidateAndThrow(FloatRangeValueObject.From(10f, 1f)));
}
