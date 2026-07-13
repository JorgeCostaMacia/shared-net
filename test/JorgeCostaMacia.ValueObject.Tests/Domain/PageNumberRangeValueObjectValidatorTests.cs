using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberRangeValueObjectValidatorTests
{
    private static readonly PageNumberRangeValueObjectValidator _validator = PageNumberRangeValueObjectValidator.Create();

    [Theory]
    [InlineData(1, 10)]
    [InlineData(3, 3)]     // start == end is allowed
    public void StartNotAfterEnd_Passes(int start, int end)
        => Assert.True(_validator.Validate(PageNumberRangeValueObject.From(start, end)).IsValid);

    [Fact]
    public void StartAfterEnd_Fails()
        => Assert.False(_validator.Validate(PageNumberRangeValueObject.From(10, 1)).IsValid);

    [Fact]
    public void NonPositiveEndpoint_Fails()
        // inner PageNumber validator rejects 0 (GreaterThan(0))
        => Assert.False(_validator.Validate(PageNumberRangeValueObject.From(0, 5)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<PageNumberRangeValueObjectValidationException>(() => _validator.ValidateAndThrow(PageNumberRangeValueObject.From(10, 1)));
}
