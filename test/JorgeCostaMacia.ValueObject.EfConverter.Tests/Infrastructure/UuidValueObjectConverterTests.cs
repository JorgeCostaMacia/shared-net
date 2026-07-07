using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class UuidValueObjectConverterTests
{
    [Fact]
    public void RoundTrips()
    {
        Guid id = Guid.NewGuid();
        UuidValueObjectConverter<TestUuid> converter = new();

        Assert.Equal(id, (Guid)converter.ConvertToProvider(new TestUuid(id))!);
        Assert.Equal(id, ((TestUuid)converter.ConvertFromProvider(id)!).Value);
    }

    public record TestUuid : UuidValueObject { public TestUuid(Guid value) : base(value) { } }
}
