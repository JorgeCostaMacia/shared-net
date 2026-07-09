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
}
