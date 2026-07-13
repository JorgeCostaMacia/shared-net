using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class StringValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutNormalizing()
        => Assert.Equal("  hi  ", new StringValueObject("  hi  ").Value);

    [Fact]
    public void From_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.From("  hi  ").Value);

    [Fact]
    public void Create_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.Create("  hi  ").Value);

    // The base validator has no rules, so Create never throws — not even on empty input.
    [Fact]
    public void Create_BaseHasNoRules_DoesNotThrowOnEmpty()
        => Assert.Equal("", StringValueObject.Create("").Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        string value = StringValueObject.Create("hi");
        Assert.Equal("hi", value);
    }

    [Fact]
    public void Equality_SameTypeSameValue_AreEqual()
        => Assert.Equal(EmailValueObject.Create("a@b.com"), EmailValueObject.Create("a@b.com"));

    [Fact]
    public void Equality_DifferentVoTypesSameValue_AreNotEqual()
        // Record equality includes the runtime type, so two different VO types with the same value differ.
        => Assert.NotEqual<StringValueObject>(EmailValueObject.From("abc"), UrlValueObject.From("abc"));

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromOtherTypes_FunnelsThroughRoot()
    {
        Assert.Equal("42", TestString.Convert(42));
        Assert.Equal("True", TestString.Convert(true));
        Assert.Equal("9", TestString.Convert(9L));
    }

    [Fact]
    public void Convert_FromFloatingPoint_UsesInvariantCulture()
    {
        Assert.Equal("2.5", TestString.Convert(2.5f));
        Assert.Equal("2.5", TestString.Convert(2.5d));
        Assert.Equal("2.5", TestString.Convert(2.5m));
    }

    [Fact]
    public void Convert_FromGuid_UsesGuidString()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id.ToString(), TestString.Convert(id));
    }

    public sealed record TestString : StringValueObject
    {
        public TestString(string value) : base(value) { }

        public static new string Convert(int value) => StringValueObject.Convert(value);

        public static new string Convert(float value) => StringValueObject.Convert(value);

        public static new string Convert(decimal value) => StringValueObject.Convert(value);

        public static new string Convert(bool value) => StringValueObject.Convert(value);

        public static new string Convert(long value) => StringValueObject.Convert(value);

        public static new string Convert(double value) => StringValueObject.Convert(value);

        public static new string Convert(Guid value) => StringValueObject.Convert(value);
    }
}
