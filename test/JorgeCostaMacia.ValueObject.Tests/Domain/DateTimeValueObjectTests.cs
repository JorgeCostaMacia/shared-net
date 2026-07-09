using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutValidating()
        => Assert.Equal(default, new DateTimeValueObject(default).Value);

    [Fact]
    public void From_PreservesKind_DoesNotForceUtc()
    {
        DateTime local = new(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        DateTimeValueObject valueObject = DateTimeValueObject.From(local);

        Assert.Equal(DateTimeKind.Local, valueObject.Value.Kind);   // not forced to UTC
        Assert.Equal(local, valueObject.Value);                     // not shifted
    }

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal(default, DateTimeValueObject.From(default).Value);

    [Fact]
    public void Create_PreservesKind_DoesNotForceUtc()
    {
        DateTime local = new(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        DateTimeValueObject valueObject = DateTimeValueObject.Create(local);

        Assert.Equal(DateTimeKind.Local, valueObject.Value.Kind);
        Assert.Equal(local, valueObject.Value);
    }

    [Fact]
    public void Create_FromUnspecified_StaysUnspecified()
    {
        DateTime unspecified = new(2026, 6, 29, 12, 0, 0);

        Assert.Equal(DateTimeKind.Unspecified, DateTimeValueObject.Create(unspecified).Value.Kind);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsDateTimeValueObjectValidationException()
        => Assert.Throws<DateTimeValueObjectValidationException>(() => DateTimeValueObject.Create(new DateTime(1800, 1, 1)));

    [Fact]
    public void Create_OnDefault_ReportsAllFailuresInOneException()
    {
        // default(DateTime) violates both NotEmpty and the 1900 minimum — one exception, the complete failure list.
        DateTimeValueObjectValidationException exception =
            Assert.Throws<DateTimeValueObjectValidationException>(() => DateTimeValueObject.Create(default));

        Assert.Equal(2, exception.Validations.Count);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        DateTime moment = new(2020, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        DateTime value = DateTimeValueObject.Create(moment);
        Assert.Equal(moment, value);
    }
}
