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

    // The zeros other systems write when they have no date: .NET's default, SQL Server's datetime minimum,
    // Delphi/OLE/Excel's zero and SQL Server's CAST(0 AS datetime).
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1753, 1, 1)]
    [InlineData(1899, 12, 30)]
    [InlineData(1900, 1, 1)]
    public void ZeroDateOfAnotherSystem_Fails(
        int year,
        int month,
        int day)
        => Assert.False(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<DateTimeUtcValueObjectValidationException>(() => _validator.ValidateAndThrow(DateTimeUtcValueObject.From(new DateTime(1800, 1, 1, 0, 0, 0, DateTimeKind.Utc))));

    // The floor is strict: midnight on 1900-01-01 is a zero, any later instant of that day is a date.
    [Fact]
    public void DateJustAfterTheFloor_Passes()
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(1900, 1, 1, 0, 0, 1, DateTimeKind.Utc))).IsValid);

    [Fact]
    public void DateAtMaxBoundary_Passes()
        => Assert.True(_validator.Validate(DateTimeUtcValueObject.From(DateTime.UtcNow.Date.AddYears(100))).IsValid);

    [Fact]
    public void DateAfterMaxBoundary_Fails()
        => Assert.False(_validator.Validate(DateTimeUtcValueObject.From(DateTime.UtcNow.Date.AddYears(100).AddDays(1))).IsValid);

    // The ceiling binds every date: a far-future sentinel does not pass.
    [Fact]
    public void OpenEndSentinel_Fails()
        => Assert.False(_validator.Validate(DateTimeUtcValueObject.From(new DateTime(9999, 12, 31, 0, 0, 0, DateTimeKind.Utc))).IsValid);
}
