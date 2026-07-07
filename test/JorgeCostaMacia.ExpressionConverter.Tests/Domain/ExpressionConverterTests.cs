// 'ExpressionConverter' is referenced fully-qualified (global::) so the type/namespace clash
// under the 'JorgeCostaMacia.ExpressionConverter' namespace does not bite in test code.
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
    }

    [Fact]
    public void Convert_MapsPropertiesToStringValues()
    {
        Expression<Func<Sample, bool>> expression = x => x.Name == "foo" && x.Age == 5;

        Dictionary<string, string> result = global::JorgeCostaMacia.ExpressionConverter.Domain.ExpressionConverter.Convert(expression);

        Assert.Equal("foo", result["Name"]);
        Assert.Equal("5", result["Age"]);
    }

    [Fact]
    public void ConvertBack_BuildsWorkingPredicate()
    {
        Dictionary<string, string> dictionary = new()
        {
            ["Name"] = "foo",
            ["Age"] = "5",
        };

        Func<Sample, bool> predicate = global::JorgeCostaMacia.ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(predicate(new Sample { Name = "foo", Age = 5 }));
        Assert.False(predicate(new Sample { Name = "bar", Age = 5 }));
    }

    [Fact]
    public void ConvertBack_HandlesGuidEnumAndNullable()
    {
        Guid id = Guid.NewGuid();

        Dictionary<string, string> dictionary = new()
        {
            ["Id"] = id.ToString(),
            ["Kind"] = "Green",
            ["Optional"] = "", // empty string -> null for a nullable property
        };

        Func<Sample, bool> predicate = global::JorgeCostaMacia.ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(predicate(new Sample { Id = id, Kind = Color.Green, Optional = null }));
        Assert.False(predicate(new Sample { Id = Guid.NewGuid(), Kind = Color.Green, Optional = null }));
    }

    [Fact]
    public void RoundTrip_ConvertThenConvertBack_Matches()
    {
        Expression<Func<Sample, bool>> original = x => x.Name == "foo" && x.Age == 7;

        Dictionary<string, string> dictionary = global::JorgeCostaMacia.ExpressionConverter.Domain.ExpressionConverter.Convert(original);
        Func<Sample, bool> rebuilt = global::JorgeCostaMacia.ExpressionConverter.Domain.ExpressionConverter.ConvertBack<Sample>(dictionary).Compile();

        Assert.True(rebuilt(new Sample { Name = "foo", Age = 7 }));
        Assert.False(rebuilt(new Sample { Name = "foo", Age = 8 }));
    }
}
