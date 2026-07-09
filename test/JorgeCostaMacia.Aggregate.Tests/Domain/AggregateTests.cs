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

        Assert.Empty(aggregate.PullDomainEvents());
    }

    [Fact]
    public void AddDomainEvents_Single_AccumulatesEvent()
    {
        TestAggregate aggregate = new();
        TestEvent raised = new(1);

        aggregate.AddDomainEvents(raised);

        Assert.Equal(new IDomainEvent[] { raised }, aggregate.PullDomainEvents().ToArray());
    }

    [Fact]
    public void AddDomainEvents_Range_AccumulatesAllInOrder()
    {
        TestAggregate aggregate = new();
        TestEvent first = new(1);
        TestEvent second = new(2);

        aggregate.AddDomainEvents([first, second]);

        Assert.Equal(new IDomainEvent[] { first, second }, aggregate.PullDomainEvents().ToArray());
    }

    [Fact]
    public void PullDomainEvents_ClearsTheListAfterPulling()
    {
        TestAggregate aggregate = new();
        aggregate.AddDomainEvents(new TestEvent(1));

        IDomainEvent[] firstPull = aggregate.PullDomainEvents().ToArray();
        IDomainEvent[] secondPull = aggregate.PullDomainEvents().ToArray();

        Assert.Single(firstPull);
        Assert.Empty(secondPull);
    }

    [Fact]
    public void AddDomainEvents_NullSingleEvent_Throws()
    {
        TestAggregate aggregate = new();

        Assert.Throws<ArgumentNullException>(() => aggregate.AddDomainEvents((IDomainEvent)null!));
    }

    [Fact]
    public void AddDomainEvents_NullCollection_Throws()
    {
        TestAggregate aggregate = new();

        Assert.Throws<ArgumentNullException>(() => aggregate.AddDomainEvents((IEnumerable<IDomainEvent>)null!));
    }

    [Fact]
    public void AddDomainEvents_EmptyRange_KeepsListEmpty()
    {
        TestAggregate aggregate = new();

        aggregate.AddDomainEvents([]);

        Assert.Empty(aggregate.PullDomainEvents());
    }

    [Fact]
    public void PullDomainEvents_ReturnedSnapshot_IsUnaffectedBySubsequentAdd()
    {
        TestAggregate aggregate = new();
        aggregate.AddDomainEvents(new TestEvent(1));

        IEnumerable<IDomainEvent> snapshot = aggregate.PullDomainEvents();
        aggregate.AddDomainEvents(new TestEvent(2));

        Assert.Single(snapshot);
    }

    [Fact]
    public void AddThenPullThenAdd_AccumulatesFreshEventsOnly()
    {
        TestAggregate aggregate = new();
        aggregate.AddDomainEvents(new TestEvent(1));
        aggregate.PullDomainEvents();

        TestEvent second = new(2);
        aggregate.AddDomainEvents(second);

        Assert.Equal(new IDomainEvent[] { second }, aggregate.PullDomainEvents().ToArray());
    }

    [Fact]
    public void AddDomainEvents_SameInstanceTwice_PreservesDuplicates()
    {
        TestAggregate aggregate = new();
        TestEvent raised = new(1);

        aggregate.AddDomainEvents(raised);
        aggregate.AddDomainEvents(raised);

        Assert.Equal(2, aggregate.PullDomainEvents().Count());
    }

    [Fact]
    public void AddDomainEvents_MixedSingleAndRange_PreservesOverallOrder()
    {
        TestAggregate aggregate = new();
        TestEvent first = new(1);
        TestEvent second = new(2);
        TestEvent third = new(3);
        TestEvent fourth = new(4);

        aggregate.AddDomainEvents(first);
        aggregate.AddDomainEvents([second, third]);
        aggregate.AddDomainEvents(fourth);

        Assert.Equal(new IDomainEvent[] { first, second, third, fourth }, aggregate.PullDomainEvents().ToArray());
    }
}
