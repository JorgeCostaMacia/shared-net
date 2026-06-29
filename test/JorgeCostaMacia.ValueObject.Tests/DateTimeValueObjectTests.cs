using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class DateTimeValueObjectTests
{
    [Fact]
    public void Create_PreservesKind_DoesNotForceUtc()
    {
        DateTime local = new(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        DateTimeValueObject valueObject = DateTimeValueObject.Create(local);

        Assert.Equal(DateTimeKind.Local, valueObject.Value.Kind);   // not forced to UTC
        Assert.Equal(local, valueObject.Value);                     // not shifted
    }

    [Fact]
    public void Create_FromUnspecified_StaysUnspecified()
    {
        DateTime unspecified = new(2026, 6, 29, 12, 0, 0);

        Assert.Equal(DateTimeKind.Unspecified, DateTimeValueObject.Create(unspecified).Value.Kind);
    }
}
