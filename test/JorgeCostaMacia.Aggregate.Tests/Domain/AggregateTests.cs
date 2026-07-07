using JorgeCostaMacia.DomainEvent.Domain;

namespace JorgeCostaMacia.Aggregate.Tests.Domain;

// 'Aggregate' is referenced from its package namespace so the base class isn't shadowed by
// the enclosing 'JorgeCostaMacia.Aggregate' namespace in test code.
file sealed record TestEvent(int Id) : IDomainEvent;

file sealed class TestAggregate : Aggregate.Domain.Aggregate;

public class AggregateTests
{
    [Fact]
    public void NewAggregate_HasNoPendingEvents()
    {
        TestAggregate aggregate = new();

        Assert.Empty(aggregate.PullAggregateEvents());
    }

    [Fact]
    public void AddAggregateEvents_Single_AccumulatesEvent()
    {
        TestAggregate aggregate = new();
        TestEvent raised = new(1);

        aggregate.AddAggregateEvents(raised);

        Assert.Equal(new IDomainEvent[] { raised }, aggregate.PullAggregateEvents().ToArray());
    }

    [Fact]
    public void AddAggregateEvents_Range_AccumulatesAllInOrder()
    {
        TestAggregate aggregate = new();
        TestEvent first = new(1);
        TestEvent second = new(2);

        aggregate.AddAggregateEvents([first, second]);

        Assert.Equal(new IDomainEvent[] { first, second }, aggregate.PullAggregateEvents().ToArray());
    }

    [Fact]
    public void PullAggregateEvents_ClearsTheListAfterPulling()
    {
        TestAggregate aggregate = new();
        aggregate.AddAggregateEvents(new TestEvent(1));

        IDomainEvent[] firstPull = aggregate.PullAggregateEvents().ToArray();
        IDomainEvent[] secondPull = aggregate.PullAggregateEvents().ToArray();

        Assert.Single(firstPull);
        Assert.Empty(secondPull);
    }
}
