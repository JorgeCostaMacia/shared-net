using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeRangeValueObjectValidatorTests
{
    private static readonly DateTimeRangeValueObjectValidator Validator = new(new DateTimeValueObjectValidator());

    private static readonly DateTime Early = new(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime Late = new(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void StartBeforeEnd_Passes()
        => Assert.True(Validator.Validate(DateTimeRangeValueObject.Create(Early, Late)).IsValid);

    [Fact]
    public void StartEqualsEnd_Passes()
        => Assert.True(Validator.Validate(DateTimeRangeValueObject.Create(Early, Early)).IsValid);

    [Fact]
    public void StartAfterEnd_Fails()
        => Assert.False(Validator.Validate(DateTimeRangeValueObject.Create(Late, Early)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<DateTimeRangeValueObjectValidationException>(() => Validator.ValidateAndThrow(DateTimeRangeValueObject.Create(Late, Early)));
}
