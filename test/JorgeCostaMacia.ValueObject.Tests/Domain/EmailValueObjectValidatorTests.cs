using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class EmailValueObjectValidatorTests
{
    private static readonly EmailValueObjectValidator Validator = EmailValueObjectValidator.Create();

    [Theory]
    [InlineData("user@host.com")]
    [InlineData("a.b+tag@sub.domain.co")]
    public void ValidEmail_Passes(string value)
        => Assert.True(Validator.Validate(EmailValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]
    [InlineData("notanemail")]
    [InlineData("no-at-sign.com")]
    public void InvalidEmail_Fails(string value)
        => Assert.False(Validator.Validate(EmailValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<EmailValueObjectValidationException>(() => Validator.ValidateAndThrow(EmailValueObject.From("notanemail")));

    [Fact]
    public void InjectableCtor_ComposesLikeCreate()
        => Assert.False(new EmailValueObjectValidator(new StringValueObjectValidator()).Validate(EmailValueObject.From("notanemail")).IsValid);
}
