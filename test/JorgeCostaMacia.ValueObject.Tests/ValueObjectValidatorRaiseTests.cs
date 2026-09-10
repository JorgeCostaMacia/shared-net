using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

/// <summary>
/// Contract test over every validator: when validation fails, each one must raise <b>its own</b> typed
/// exception rather than FluentValidation's or the family base's. That override is what lets a consumer
/// catch <c>EmailValueObjectValidationException</c> instead of inspecting a generic failure, and nine of
/// the validators carry no rules of their own, so no ordinary test can ever make them fail — the
/// override is invoked here directly with a synthetic failing result.
/// </summary>
public class ValueObjectValidatorRaiseTests
{
    private static readonly Type[] _validators = typeof(IValueObject).Assembly.GetTypes()
        .Where(type => type.Name.EndsWith("ValueObjectValidator", StringComparison.Ordinal))
        .OrderBy(type => type.Name, StringComparer.Ordinal)
        .ToArray();

    public static TheoryData<Type> Validators()
    {
        TheoryData<Type> data = new TheoryData<Type>();
        foreach (Type validator in _validators)
        {
            data.Add(validator);
        }

        return data;
    }

    [Fact]
    public void TheScan_FindsOneValidatorPerValueObject()
    {
        int valueObjects = typeof(IValueObject).Assembly.GetTypes()
            .Count(type => type is { IsClass: true, IsAbstract: false } && typeof(IValueObject).IsAssignableFrom(type));

        Assert.Equal(valueObjects, _validators.Length);
    }

    [Theory]
    [MemberData(nameof(Validators))]
    public void RaiseValidationException_ThrowsTheValidatorOwnTypedException(Type validator)
    {
        string expected = validator.Name.Replace("Validator", "ValidationException", StringComparison.Ordinal);
        MethodInfo raise = validator.GetMethod(
            "RaiseValidationException",
            BindingFlags.Instance | BindingFlags.NonPublic)
            ?? throw new InvalidOperationException($"{validator.Name}: no RaiseValidationException override.");

        object instance = Create(validator);
        ValidationResult result = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Value", "boom") });

        TargetInvocationException invocation = Assert.Throws<TargetInvocationException>(
            () => raise.Invoke(instance, new object?[] { null, result }));

        Assert.NotNull(invocation.InnerException);
        Assert.Equal(expected, invocation.InnerException.GetType().Name);
    }

    // The failures the validator collected have to survive into the exception, or a consumer catching
    // it learns that something failed but not what.
    [Theory]
    [MemberData(nameof(Validators))]
    public void RaiseValidationException_CarriesTheFailuresIntoTheException(Type validator)
    {
        MethodInfo raise = validator.GetMethod(
            "RaiseValidationException",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

        object instance = Create(validator);
        ValidationResult result = new ValidationResult(new List<ValidationFailure>
        {
            new ValidationFailure("Value", "first"),
            new ValidationFailure("Value", "second")
        });

        TargetInvocationException invocation = Assert.Throws<TargetInvocationException>(
            () => raise.Invoke(instance, new object?[] { null, result }));

        object failures = invocation.InnerException!.GetType().GetProperty("Validations")!
            .GetValue(invocation.InnerException)!;

        Assert.Equal(2, ((System.Collections.ICollection)failures).Count);
    }

    // Each validator assembles itself through a static Create() — the seam the value objects use, so a
    // validator that cannot be built that way is one no Create() can reach.
    [Theory]
    [MemberData(nameof(Validators))]
    public void EveryValidator_AssemblesItselfThroughStaticCreate(Type validator)
    {
        MethodInfo create = validator.GetMethod("Create", BindingFlags.Public | BindingFlags.Static)
            ?? throw new InvalidOperationException($"{validator.Name}: no static Create().");

        Assert.IsAssignableFrom<IValidator>(create.Invoke(null, Array.Empty<object>())!);
    }

    private static object Create(Type validator)
        => validator.GetMethod("Create", BindingFlags.Public | BindingFlags.Static)!.Invoke(null, Array.Empty<object>())!;
}
