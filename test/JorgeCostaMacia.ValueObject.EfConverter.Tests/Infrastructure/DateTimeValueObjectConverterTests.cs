using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class DateTimeValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        DateTime now = new(2026, 7, 7, 12, 0, 0, DateTimeKind.Utc);
        DateTimeValueObjectConverter<TestDateTime> converter = new();

        Assert.Equal(now, (DateTime)converter.ConvertToProvider(new TestDateTime(now))!);
        Assert.Equal(now, ((TestDateTime)converter.ConvertFromProvider(now)!).Value);
    }

    public record TestDateTime : DateTimeValueObject { public TestDateTime(DateTime value) : base(value) { } }
}
