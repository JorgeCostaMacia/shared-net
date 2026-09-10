using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class EmailValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizingOrValidating()
        => Assert.Equal("  notanemail  ", new EmailValueObject("  notanemail  ").Value);

    [Fact]
    public void From_Trims()
        => Assert.Equal("user@host.com", EmailValueObject.From("  user@host.com  ").Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal("notanemail", EmailValueObject.From("notanemail").Value);

    [Fact]
    public void Create_Trims()
        => Assert.Equal("user@host.com", EmailValueObject.Create("  user@host.com  ").Value);

    [Fact]
    public void Create_OnInvalid_ThrowsEmailValueObjectValidationException()
        => Assert.Throws<EmailValueObjectValidationException>(() => EmailValueObject.Create("notanemail"));

    [Fact]
    public void Create_OnEmpty_ReportsAllFailuresInOneException()
    {
        // Empty violates both rules (NotEmpty + EmailAddress) — one exception, the complete failure list.
        EmailValueObjectValidationException exception =
            Assert.Throws<EmailValueObjectValidationException>(() => EmailValueObject.Create(""));

        Assert.Equal(2, exception.Validations.Count);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out. It is not a second
    // policy for invalid input — a supplied value still goes through the same rules.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(EmailValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.Equal("user@host.com", EmailValueObject.FromOrNull("  user@host.com  ")!.Value);

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(EmailValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<EmailValueObjectValidationException>(() => EmailValueObject.CreateOrNull("notanemail"));

    [Fact]
    public void CreateOrNull_WithAValidValue_ReturnsTheValueObject()
        => Assert.Equal("user@host.com", EmailValueObject.CreateOrNull("user@host.com")!.Value);
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("user@host.com", EmailValueObject.From("user@host.com").ToString());
}
