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
        TestAggregate aggregate = new TestAggregate();

        Assert.Empty(aggregate.PullEvents());
    }

    [Fact]
    public void AddEvent_AccumulatesEvent()
    {
        TestAggregate aggregate = new TestAggregate();
        TestEvent raised = new TestEvent(1);

        aggregate.AddEvent(raised);

        Assert.Equal(new IDomainEvent[] { raised }, aggregate.PullEvents().ToArray());
    }

    [Fact]
    public void AddEvents_AccumulatesAllInOrder()
    {
        TestAggregate aggregate = new TestAggregate();
        TestEvent first = new TestEvent(1);
        TestEvent second = new TestEvent(2);

        aggregate.AddEvents(new IDomainEvent[] { first, second });

        Assert.Equal(new IDomainEvent[] { first, second }, aggregate.PullEvents().ToArray());
    }

    [Fact]
    public void PullEvents_ClearsTheListAfterPulling()
    {
        TestAggregate aggregate = new TestAggregate();
        aggregate.AddEvent(new TestEvent(1));

        IDomainEvent[] firstPull = aggregate.PullEvents().ToArray();
        IDomainEvent[] secondPull = aggregate.PullEvents().ToArray();

        Assert.Single(firstPull);
        Assert.Empty(secondPull);
    }

    [Fact]
    public void AddEvent_NullEvent_Throws()
    {
        TestAggregate aggregate = new TestAggregate();

        Assert.Throws<ArgumentNullException>(() => aggregate.AddEvent(null!));
    }

    [Fact]
    public void AddEvents_NullCollection_Throws()
    {
        TestAggregate aggregate = new TestAggregate();

        Assert.Throws<ArgumentNullException>(() => aggregate.AddEvents(null!));
    }

    [Fact]
    public void AddEvents_EmptyRange_KeepsListEmpty()
    {
        TestAggregate aggregate = new TestAggregate();

        aggregate.AddEvents(new List<IDomainEvent>());

        Assert.Empty(aggregate.PullEvents());
    }

    [Fact]
    public void PullEvents_ReturnedSnapshot_IsUnaffectedBySubsequentAdd()
    {
        TestAggregate aggregate = new TestAggregate();
        aggregate.AddEvent(new TestEvent(1));

        IEnumerable<IDomainEvent> snapshot = aggregate.PullEvents();
        aggregate.AddEvent(new TestEvent(2));

        Assert.Single(snapshot);
    }

    [Fact]
    public void AddThenPullThenAdd_AccumulatesFreshEventsOnly()
    {
        TestAggregate aggregate = new TestAggregate();
        aggregate.AddEvent(new TestEvent(1));
        aggregate.PullEvents();

        TestEvent second = new TestEvent(2);
        aggregate.AddEvent(second);

        Assert.Equal(new IDomainEvent[] { second }, aggregate.PullEvents().ToArray());
    }

    [Fact]
    public void AddEvent_SameInstanceTwice_PreservesDuplicates()
    {
        TestAggregate aggregate = new TestAggregate();
        TestEvent raised = new TestEvent(1);

        aggregate.AddEvent(raised);
        aggregate.AddEvent(raised);

        Assert.Equal(2, aggregate.PullEvents().Count());
    }

    [Fact]
    public void AddEventAndAddEvents_MixedSingleAndRange_PreservesOverallOrder()
    {
        TestAggregate aggregate = new TestAggregate();
        TestEvent first = new TestEvent(1);
        TestEvent second = new TestEvent(2);
        TestEvent third = new TestEvent(3);
        TestEvent fourth = new TestEvent(4);

        aggregate.AddEvent(first);
        aggregate.AddEvents(new IDomainEvent[] { second, third });
        aggregate.AddEvent(fourth);

        Assert.Equal(new IDomainEvent[] { first, second, third, fourth }, aggregate.PullEvents().ToArray());
    }
}
