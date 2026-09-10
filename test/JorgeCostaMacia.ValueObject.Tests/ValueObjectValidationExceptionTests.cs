using System.Collections.Immutable;
using System.Reflection;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

/// <summary>
/// Contract test over every typed validation exception in the library. Each one publishes three
/// constructors — the all-required transport shape, the all-nullable shape that fills in the family
/// defaults, and the convenience shape the validators throw — and each has to carry its own fixed
/// <c>AggregateCode</c>. These are the constructors a deserializer reaches for, so a new value object
/// whose exception drifts from the shape is caught here rather than at a transport boundary.
/// </summary>
public class ValueObjectValidationExceptionTests
{
    private static readonly Type[] _exceptions = typeof(IValueObject).Assembly.GetTypes()
        .Where(type => type.Name.EndsWith("ValueObjectValidationException", StringComparison.Ordinal))
        .OrderBy(type => type.Name, StringComparer.Ordinal)
        .ToArray();

    public static TheoryData<Type> Exceptions()
    {
        TheoryData<Type> data = new TheoryData<Type>();
        foreach (Type exception in _exceptions)
        {
            data.Add(exception);
        }

        return data;
    }

    [Fact]
    public void TheScan_FindsOneExceptionPerValueObject()
    {
        int valueObjects = typeof(IValueObject).Assembly.GetTypes()
            .Count(type => type is { IsClass: true, IsAbstract: false } && typeof(IValueObject).IsAssignableFrom(type));

        Assert.Equal(valueObjects, _exceptions.Length);
    }

    // The shape the validators throw: everything but the failures comes from the type's own defaults.
    [Theory]
    [MemberData(nameof(Exceptions))]
    public void FailuresConstructor_StampsTheTypeOwnCodeAndKeepsTheFailures(Type exception)
    {
        List<ValidationFailure> failures = new List<ValidationFailure> { new ValidationFailure("Value", "boom") };

        object instance = Construct(exception, new object?[] { failures, null });

        Assert.NotEqual(Guid.Empty, Read<Guid>(instance, "AggregateCode"));
        Assert.NotEqual(Guid.Empty, Read<Guid>(instance, "AggregateId"));
        Assert.Equal(400, Read<int>(instance, "AggregateHttpCode"));
        Assert.Equal(exception.FullName, Read<string>(instance, "AggregateType"));
        Assert.Equal("boom", Assert.Single(Read<ImmutableList<ValidationFailure>>(instance, "Validations")).ErrorMessage);
    }

    // Every code is distinct: two value objects failing must not report the same error identity.
    [Fact]
    public void EveryException_CarriesADistinctCode()
    {
        List<ValidationFailure> failures = new List<ValidationFailure> { new ValidationFailure("Value", "boom") };
        List<Guid> codes = new List<Guid>();
        foreach (Type exception in _exceptions)
        {
            codes.Add(Read<Guid>(Construct(exception, new object?[] { failures, null }), "AggregateCode"));
        }

        Assert.Equal(codes.Count, codes.Distinct().Count());
    }

    // The all-nullable shape: nulls mean "apply the family default", not "store null".
    [Theory]
    [MemberData(nameof(Exceptions))]
    public void NullableConstructor_WithAllNulls_AppliesTheDefaults(Type exception)
    {
        ConstructorInfo constructor = ConstructorWith(exception, 8, typeof(IEnumerable<ValidationFailure>));

        object instance = constructor.Invoke(new object?[]
        {
            null, null, null, null, null, null, null, new List<ValidationFailure>()
        });

        Assert.NotEqual(Guid.Empty, Read<Guid>(instance, "AggregateId"));
        Assert.NotEqual(Guid.Empty, Read<Guid>(instance, "AggregateCode"));
        Assert.Equal(400, Read<int>(instance, "AggregateHttpCode"));
        Assert.NotEqual(default, Read<DateTime>(instance, "AggregateOccurredAt"));
    }

    // The transport shape: what goes in comes back out untouched, which is what rehydration relies on.
    [Theory]
    [MemberData(nameof(Exceptions))]
    public void RequiredConstructor_PreservesEveryValueVerbatim(Type exception)
    {
        Guid aggregateId = Guid.NewGuid();
        Guid aggregateCode = Guid.NewGuid();
        DateTime occurredAt = new DateTime(2026, 9, 10, 12, 0, 0, DateTimeKind.Utc);
        ConstructorInfo constructor = ConstructorWith(exception, 8, typeof(ImmutableList<ValidationFailure>));

        object instance = constructor.Invoke(new object?[]
        {
            aggregateId, "Some.Type", aggregateCode, 422, occurredAt, "message", null,
            ImmutableList<ValidationFailure>.Empty
        });

        Assert.Equal(aggregateId, Read<Guid>(instance, "AggregateId"));
        Assert.Equal("Some.Type", Read<string>(instance, "AggregateType"));
        Assert.Equal(aggregateCode, Read<Guid>(instance, "AggregateCode"));
        Assert.Equal(422, Read<int>(instance, "AggregateHttpCode"));
        Assert.Equal(occurredAt, Read<DateTime>(instance, "AggregateOccurredAt"));
    }

    private static object Construct(Type exception, object?[] arguments)
        => Activator.CreateInstance(exception, arguments)
            ?? throw new InvalidOperationException($"{exception.Name}: could not be constructed.");

    private static ConstructorInfo ConstructorWith(Type exception, int parameterCount, Type lastParameter)
        => exception.GetConstructors()
            .Single(constructor =>
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                return parameters.Length == parameterCount && parameters[^1].ParameterType == lastParameter;
            });

    private static T Read<T>(object instance, string property)
        => (T)instance.GetType().GetProperty(property)!.GetValue(instance)!;
}
