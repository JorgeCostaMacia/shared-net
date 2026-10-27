using FluentValidation;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class EmailValueObjectValidatorTests
{
    private static readonly EmailValueObjectValidator _validator = EmailValueObjectValidator.Create();

    // The base takes every address the standard does, the rare ones included: narrowing it is a derived Value Object's
    // job.
    [Theory]
    [InlineData("user@host.com")]
    [InlineData("a.b+tag@sub.domain.co")]
    [InlineData("User@Host.COM")]          // case is kept, not rejected
    [InlineData("a@localhost")]            // dotless domain
    [InlineData("a@[1.2.3.4]")]            // IP literal
    [InlineData("\"a b\"@c.com")]          // quoted local part
    [InlineData("jose@españa.es")]         // non-ASCII domain
    public void AnyEmail_Passes(string value)
        => Assert.True(_validator.Validate(EmailValueObject.From(value)).IsValid);

    [Theory]
    [InlineData("")]                       // empty
    [InlineData("notanemail")]             // no @
    [InlineData("no-at-sign.com")]         // no @
    [InlineData("a@")]                     // no domain
    [InlineData("@b.com")]                 // no local part
    [InlineData("a@b@c.com")]              // two @
    [InlineData("a b@c.com")]              // MailAddress reads it as the display name "a" plus b@c.com
    [InlineData("Name <a@b.com>")]         // display name around the address
    [InlineData("a@b.com, c@d.com")]       // a list, not an address
    [InlineData("a..b@c.com")]             // consecutive dots
    [InlineData(".a@b.com")]               // leading dot
    [InlineData("a@b..com")]               // empty domain label
    [InlineData("a@b c.com")]              // space in the domain
    public void NotAnEmail_Fails(string value)
        => Assert.False(_validator.Validate(EmailValueObject.From(value)).IsValid);

    // No cascade: an empty value reports both what it is (empty) and what it is not (an email).
    [Fact]
    public void Empty_ReportsBothRules()
    {
        ValidationResult result = _validator.Validate(EmailValueObject.From(""));

        Assert.Equal(2, result.Errors.Count);
        Assert.Contains(result.Errors, e => e.ErrorCode == "EmailValidator");
    }

    [Fact]
    public void Invalid_ValidateAndThrow_ThrowsTypedException()
        => Assert.Throws<EmailValueObjectValidationException>(
            () => _validator.ValidateAndThrow(EmailValueObject.From("a b@c.com")));

    [Fact]
    public void InjectableCtor_ComposesLikeCreate()
        => Assert.False(
            new EmailValueObjectValidator(new StringValueObjectValidator())
                .Validate(EmailValueObject.From("notanemail"))
                .IsValid);
}
