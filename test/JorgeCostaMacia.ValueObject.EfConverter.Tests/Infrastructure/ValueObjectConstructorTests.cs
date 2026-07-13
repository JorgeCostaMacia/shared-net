using JorgeCostaMacia.ValueObject.Domain;
using JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

namespace JorgeCostaMacia.ValueObject.EfConverter.Tests.Infrastructure;

public class ValueObjectConstructorTests
{
    [Fact]
    public void FromProvider_CallsTheConstructor_NotCreate()
    {
        IntValueObjectConverter<TestCreateThrows> converter = new IntValueObjectConverter<TestCreateThrows>();

        // Create throws; if the converter used it this would blow up. It uses the constructor instead.
        Assert.Equal(7, ((TestCreateThrows)converter.ConvertFromProvider(7)!).Value);
    }

    [Fact]
    public void FromProvider_UsesNonPublicConstructor()
    {
        StringValueObjectConverter<TestPrivateCtor> converter = new StringValueObjectConverter<TestPrivateCtor>();

        Assert.Equal("x", ((TestPrivateCtor)converter.ConvertFromProvider("x")!).Value);
    }

    [Fact]
    public void Constructing_Throws_WhenNoSingleValueConstructorExists()
    {
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => new IntValueObjectConverter<TestNoSingleValueCtor>());

        Assert.Contains("needs a constructor taking a single", exception.Message);
    }

    public record TestCreateThrows : IntValueObject
    {
        public TestCreateThrows(int value) : base(value) { }

        public static new TestCreateThrows Create(int value) => throw new InvalidOperationException("Create must not be called by the converter.");
    }

    public record TestPrivateCtor : StringValueObject
    {
        private TestPrivateCtor(string value) : base(value) { }

        public static new TestPrivateCtor Create(string value) => new TestPrivateCtor(value);
    }

    // Derives from a family base but has NO single-int constructor, so rehydration cannot find one.
    public record TestNoSingleValueCtor : IntValueObject
    {
        public TestNoSingleValueCtor(int value, string _) : base(value) { }
    }
}
