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

    // The zeros other systems write when they have no date: .NET's default and Delphi/OLE/Excel's.
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1899, 12, 30)]
    public void ZeroDateOfAnotherSystem_Fails(
        int year,
        int month,
        int day)
        => Assert.False(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<DateTimeUtcValueObjectValidationException>(() => _validator.ValidateAndThrow(DateTimeUtcValueObject.From(new DateTime(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc))));

    [Fact]
    public void DateAtMinBoundary_Passes()
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    // No ceiling in the base: a field that needs one declares it in its own Value Object.
    [Theory]
    [InlineData(2190, 1, 1)]
    [InlineData(9999, 12, 31)]
    public void FarFutureDate_Passes(
        int year,
        int month,
        int day)
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc))).IsValid);
}
