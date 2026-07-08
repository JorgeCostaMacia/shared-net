using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class EmailValueObjectValidatorTests
{
    private static readonly EmailValueObjectValidator Validator = new(new StringValueObjectValidator());

    [Theory]
    [InlineData("user@host.com")]
    [InlineData("a.b+tag@sub.domain.co")]
    public void ValidEmail_Passes(string value)
        => Assert.True(Validator.Validate(EmailValueObject.Create(value)).IsValid);

    [Theory]
    [InlineData("")]
    [InlineData("notanemail")]
    [InlineData("no-at-sign.com")]
    public void InvalidEmail_Fails(string value)
        => Assert.False(Validator.Validate(EmailValueObject.Create(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<EmailValueObjectValidationException>(() => Validator.ValidateAndThrow(EmailValueObject.Create("notanemail")));
}
