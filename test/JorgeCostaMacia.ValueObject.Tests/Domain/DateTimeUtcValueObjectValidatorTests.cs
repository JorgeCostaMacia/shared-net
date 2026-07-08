using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeUtcValueObjectValidatorTests
{
    private static readonly DateTimeUtcValueObjectValidator Validator = new();

    [Fact]
    public void DateWithinRange_Passes()
        => Assert.True(Validator.Validate(DateTimeUtcValueObject.Create(new DateTime(2020, 6, 15, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateBefore1900_Fails()
        => Assert.False(Validator.Validate(DateTimeUtcValueObject.Create(new DateTime(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateTooFarInFuture_Fails()
        => Assert.False(Validator.Validate(DateTimeUtcValueObject.Create(DateTime.UtcNow.AddYears(200))).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<DateTimeUtcValueObjectValidationException>(() => Validator.ValidateAndThrow(DateTimeUtcValueObject.Create(new DateTime(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc))));

    [Fact]
    public void DateAtMinBoundary_Passes()
        => Assert.True(Validator.Validate(DateTimeUtcValueObject.Create(new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateAtMaxBoundary_Passes()
        => Assert.True(Validator.Validate(DateTimeUtcValueObject.Create(DateTime.UtcNow.Date.AddYears(100))).IsValid);
}
