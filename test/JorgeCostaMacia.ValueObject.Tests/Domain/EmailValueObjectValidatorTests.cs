using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class EmailValueObjectValidatorTests
{
    private static readonly EmailValueObjectValidator _validator = EmailValueObjectValidator.Create();

    [Theory]
    [InlineData("user@host.com")]
    [InlineData("a.b+tag@sub.domain.co")]
    public void ValidEmail_Passes(string value)
        => Assert.True(_validator.Validate(EmailValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]
    [InlineData("notanemail")]
    [InlineData("no-at-sign.com")]
    public void InvalidEmail_Fails(string value)
        => Assert.False(_validator.Validate(EmailValueObject.From(value)).IsValid);

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<EmailValueObjectValidationException>(() => _validator.ValidateAndThrow(EmailValueObject.From("notanemail")));

    [Fact]
    public void InjectableCtor_ComposesLikeCreate()
        => Assert.False(new EmailValueObjectValidator(new StringValueObjectValidator()).Validate(EmailValueObject.From("notanemail")).IsValid);
}
