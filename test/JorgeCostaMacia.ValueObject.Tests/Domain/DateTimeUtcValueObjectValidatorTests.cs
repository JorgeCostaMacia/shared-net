using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeUtcValueObjectValidatorTests
{
    private static readonly DateTimeUtcValueObjectValidator _validator = DateTimeUtcValueObjectValidator.Create();

    [Fact]
    public void DateWithinRange_Passes()
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(2020, 6, 15, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateBefore1900_Fails()
        => Assert.False(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateTooFarInFuture_Fails()
        => Assert.False(_validator.Validate(DateTimeUtcValueObject.From(DateTime.UtcNow.AddYears(200))).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<DateTimeUtcValueObjectValidationException>(() => _validator.ValidateAndThrow(DateTimeUtcValueObject.From(new DateTime(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc))));

    [Fact]
    public void DateAtMinBoundary_Passes()
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateAtMaxBoundary_Passes()
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(DateTime.UtcNow.Date.AddYears(100))).IsValid);
}
