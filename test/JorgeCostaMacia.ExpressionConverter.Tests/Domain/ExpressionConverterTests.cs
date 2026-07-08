// 'ExpressionConverter' is referenced from its package namespace ('ExpressionConverter.Domain.')
// so the type/namespace clash under 'JorgeCostaMacia.ExpressionConverter' does not bite in test code.
using System.Linq.Expressions;

namespace JorgeCostaMacia.ExpressionConverter.Tests.Domain;

public class ExpressionConverterTests
{
    private enum Color
    {
        Red,
        Green,
        Blue,
    }

    private sealed class Sample
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public Guid Id { get; set; }
        public Color Kind { get; set; }
        public int? Optional { get; set; }
        public bool Flag { get; set; }
        public DateTime When { get; set; }
        public Sample? Child { get; set; }   // reference type, not IConvertible — used for the non-convertible case
    }

    // ---- Convert ----

    [Fact]
    public void Convert_SingleClause_ReturnsOneEntry()
    {
        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Age == 5);

        Assert.Single(result);
        Assert.Equal("5", result["Age"]);
    }

    [Fact]
    public void Convert_ThreeClauses_ReturnsAllThree()
    {
        Guid id = Guid.NewGuid();
        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(
            x => x.Name == "foo" && x.Age == 5 && x.Id == id);

        Assert.Equal(3, result.Count);
        Assert.Equal("foo", result["Name"]);
        Assert.Equal("5", result["Age"]);
        Assert.Equal(id.ToString(), result["Id"]);
    }

    [Fact]
    public void Convert_CapturedVariableRhs_IsEvaluated()
    {
        string name = "captured";
        int age = 42;

        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Name == name && x.Age == age);

        Assert.Equal("captured", result["Name"]);
        Assert.Equal("42", result["Age"]);
    }

    [Fact]
    public void Convert_Enum_IsSerializedByName()
    {
        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Kind == Color.Green);

        Assert.Equal("Green", result["Kind"]);
    }

    [Fact]
    public void Convert_NullConstant_MapsToEmptyString()
    {
        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Name == null);

        Assert.Equal("", result["Name"]);
    }

    [Fact]
    public void Convert_DateTime_UsesRoundTripFormat()
    {
        DateTime when = new(2020, 1, 1, 12, 0, 0, 500, DateTimeKind.Utc);

        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.When == when);

        Assert.Equal(when.ToString("O", System.Globalization.CultureInfo.InvariantCulture), result["When"]);
    }

    [Fact]
    public void Convert_ExplicitCastOnRhs_IsApplied()
    {
        double d = 3.9;

        Dictionary<string, string> result = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Age == (int)d);

        Assert.Equal("3", result["Age"]);   // the (int) cast is applied, not discarded
    }

    [Fact]
    public void Convert_DuplicateProperty_Throws()
        => Assert.Throws<ArgumentException>(() => ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Age == 1 && x.Age == 2));

    [Fact]
    public void Convert_UnsupportedOperator_Throws()
        => Assert.Throws<InvalidCastException>(() => ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Age > 5));

    // ---- ConvertBack ----

    [Fact]
    public void ConvertBack_EmptyDictionary_MatchesEverything()
    {
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new()).Compile();

        Assert.True(predicate(new Sample()));
        Assert.True(predicate(new Sample { Name = "anything", Age = 99 }));
    }

    [Fact]
    public void ConvertBack_SingleEntry_BuildsPredicate()
    {
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Age"] = "5" }).Compile();

        Assert.True(predicate(new Sample { Age = 5 }));
        Assert.False(predicate(new Sample { Age = 6 }));
    }

    [Fact]
    public void ConvertBack_UnknownKey_Throws()
        => Assert.Throws<ArgumentException>(() => ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Nope"] = "1" }));

    [Fact]
    public void ConvertBack_NullableWithValue_Unwraps()
    {
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Optional"] = "7" }).Compile();

        Assert.True(predicate(new Sample { Optional = 7 }));
        Assert.False(predicate(new Sample { Optional = null }));
    }

    [Fact]
    public void ConvertBack_NullableEmpty_MapsToNull()
    {
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Optional"] = "" }).Compile();

        Assert.True(predicate(new Sample { Optional = null }));
        Assert.False(predicate(new Sample { Optional = 0 }));
    }

    // ---- Round-trips (Convert -> ConvertBack) ----

    [Fact]
    public void RoundTrip_Enum_Matches()
    {
        Dictionary<string, string> dictionary = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Kind == Color.Blue);
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(predicate(new Sample { Kind = Color.Blue }));
        Assert.False(predicate(new Sample { Kind = Color.Red }));
    }

    [Fact]
    public void RoundTrip_Guid_Matches()
    {
        Guid id = Guid.NewGuid();
        Dictionary<string, string> dictionary = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Id == id);
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(predicate(new Sample { Id = id }));
        Assert.False(predicate(new Sample { Id = Guid.NewGuid() }));
    }

    [Fact]
    public void RoundTrip_DateTime_PreservesValue()
    {
        DateTime when = new(2020, 1, 1, 12, 0, 0, 500, DateTimeKind.Utc);
        Dictionary<string, string> dictionary = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.When == when);
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(predicate(new Sample { When = when }));
    }

    [Fact]
    public void RoundTrip_Bool_Matches()
    {
        Dictionary<string, string> dictionary = ExpressionConverter.Domain.ExpressionConverter.Convert<Sample>(x => x.Flag == true);
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(predicate(new Sample { Flag = true }));
        Assert.False(predicate(new Sample { Flag = false }));
    }

    [Fact]
    public void ConvertBack_MultipleEntries_RequireAll()
    {
        Func<Sample, bool> predicate = ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Age"] = "5", ["Name"] = "foo" }).Compile();

        Assert.True(predicate(new Sample { Age = 5, Name = "foo" }));
        Assert.False(predicate(new Sample { Age = 5, Name = "bar" }));
        Assert.False(predicate(new Sample { Age = 6, Name = "foo" }));
    }

    [Fact]
    public void ConvertBack_UnparseableNumber_ThrowsFormatException()
        => Assert.Throws<FormatException>(() => ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Age"] = "abc" }));

    [Fact]
    public void ConvertBack_NonConvertiblePropertyType_ThrowsInvalidCastException()
        => Assert.Throws<InvalidCastException>(() => ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(new() { ["Child"] = "x" }));
}
