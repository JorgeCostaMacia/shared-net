using JorgeCostaMacia.DomainEvent.Domain;

namespace JorgeCostaMacia.Aggregate.Tests.Domain;

// 'Aggregate' is referenced from its package namespace ('Aggregate.Domain.') so the base class isn't
// shadowed by the enclosing 'JorgeCostaMacia.Aggregate' namespace in test code.
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

    [Fact]
    public void AddAggregateEvents_NullSingleEvent_Throws()
    {
        TestAggregate aggregate = new();

        Assert.Throws<ArgumentNullException>(() => aggregate.AddAggregateEvents((IDomainEvent)null!));
    }

    [Fact]
    public void AddAggregateEvents_NullCollection_Throws()
    {
        TestAggregate aggregate = new();

        Assert.Throws<ArgumentNullException>(() => aggregate.AddAggregateEvents((IEnumerable<IDomainEvent>)null!));
    }

    [Fact]
    public void AddAggregateEvents_EmptyRange_KeepsListEmpty()
    {
        TestAggregate aggregate = new();

        aggregate.AddAggregateEvents([]);

        Assert.Empty(aggregate.PullAggregateEvents());
    }

    [Fact]
    public void PullAggregateEvents_ReturnedSnapshot_IsUnaffectedBySubsequentAdd()
    {
        TestAggregate aggregate = new();
        aggregate.AddAggregateEvents(new TestEvent(1));

        IEnumerable<IDomainEvent> snapshot = aggregate.PullAggregateEvents();
        aggregate.AddAggregateEvents(new TestEvent(2));

        Assert.Single(snapshot);
    }

    [Fact]
    public void AddThenPullThenAdd_AccumulatesFreshEventsOnly()
    {
        TestAggregate aggregate = new();
        aggregate.AddAggregateEvents(new TestEvent(1));
        aggregate.PullAggregateEvents();

        TestEvent second = new(2);
        aggregate.AddAggregateEvents(second);

        Assert.Equal(new IDomainEvent[] { second }, aggregate.PullAggregateEvents().ToArray());
    }

    [Fact]
    public void AddAggregateEvents_SameInstanceTwice_PreservesDuplicates()
    {
        TestAggregate aggregate = new();
        TestEvent raised = new(1);

        aggregate.AddAggregateEvents(raised);
        aggregate.AddAggregateEvents(raised);

        Assert.Equal(2, aggregate.PullAggregateEvents().Count());
    }

    [Fact]
    public void AddAggregateEvents_MixedSingleAndRange_PreservesOverallOrder()
    {
        TestAggregate aggregate = new();
        TestEvent first = new(1);
        TestEvent second = new(2);
        TestEvent third = new(3);
        TestEvent fourth = new(4);

        aggregate.AddAggregateEvents(first);
        aggregate.AddAggregateEvents([second, third]);
        aggregate.AddAggregateEvents(fourth);

        Assert.Equal(new IDomainEvent[] { first, second, third, fourth }, aggregate.PullAggregateEvents().ToArray());
    }
}
