using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class ValueObjectConstructorTests
{
    [Fact]
    public void FromProvider_CallsTheConstructor_NotCreate()
    {
        IntValueObjectConverter<TestCreateThrows> converter = new();

        // Create throws; if the converter used it this would blow up. It uses the constructor instead.
        Assert.Equal(7, ((TestCreateThrows)converter.ConvertFromProvider(7)!).Value);
    }

    [Fact]
    public void FromProvider_UsesNonPublicConstructor()
    {
        StringValueObjectConverter<TestPrivateCtor> converter = new();

        Assert.Equal("x", ((TestPrivateCtor)converter.ConvertFromProvider("x")!).Value);
    }

    public record TestCreateThrows : IntValueObject
    {
        public TestCreateThrows(int value) : base(value) { }

        public static new TestCreateThrows Create(int value) => throw new InvalidOperationException("Create must not be called by the converter.");
    }

    public record TestPrivateCtor : StringValueObject
    {
        private TestPrivateCtor(string value) : base(value) { }

        public static new TestPrivateCtor Create(string value) => new(value);
    }
}
