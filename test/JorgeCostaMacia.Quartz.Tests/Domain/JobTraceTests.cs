using JorgeCostaMacia.Quartz.Domain;
using JorgeCostaMacia.Quartz.Tests.Fakes;

namespace JorgeCostaMacia.Quartz.Tests.Domain;

public class JobTraceTests
{
    [Fact]
    public void Create_MintsAFreshPair_CorrelationDefaultsToTheAggregate()
    {
        // the context-less path (e.g. a trigger misfire): fresh, unshared identifiers.
        JobTrace trace = JobTrace.Create();

        Assert.NotEqual(Guid.Empty, trace.AggregateId);
        Assert.Equal(trace.AggregateId, trace.CorrelationId);
    }

    [Fact]
    public void GetOrCreate_NoIdentifiers_MintsAndStoresThePair()
    {
        JobExecutionContextFake context = new JobExecutionContextFake();

        JobTrace trace = JobTrace.GetOrCreate(context);

        Assert.NotEqual(Guid.Empty, trace.AggregateId);
        Assert.Equal(trace.AggregateId, trace.CorrelationId);   // a fresh correlation defaults to the aggregate id
        Assert.Equal(trace.AggregateId, context.Get("AggregateId"));
        Assert.Equal(trace.CorrelationId, context.Get("CorrelationId"));
    }

    [Fact]
    public void GetOrCreate_ExistingIdentifiers_ReturnsThem()
    {
        JobExecutionContextFake context = new JobExecutionContextFake();
        Guid aggregateId = Guid.NewGuid();
        Guid correlationId = Guid.NewGuid();
        context.Put("AggregateId", aggregateId);
        context.Put("CorrelationId", correlationId);

        JobTrace trace = JobTrace.GetOrCreate(context);

        Assert.Equal(aggregateId, trace.AggregateId);
        Assert.Equal(correlationId, trace.CorrelationId);
    }

    [Fact]
    public void GetOrCreate_ExistingAggregateOnly_CorrelationDefaultsToIt()
    {
        JobExecutionContextFake context = new JobExecutionContextFake();
        Guid aggregateId = Guid.NewGuid();
        context.Put("AggregateId", aggregateId);

        JobTrace trace = JobTrace.GetOrCreate(context);

        Assert.Equal(aggregateId, trace.AggregateId);
        Assert.Equal(aggregateId, trace.CorrelationId);
    }

    [Fact]
    public void GetOrCreate_CalledBySeveralObservers_ConvergesOnTheFirstPair()
    {
        // the idempotence every listener relies on: no registration-order contract.
        JobExecutionContextFake context = new JobExecutionContextFake();

        JobTrace first = JobTrace.GetOrCreate(context);
        JobTrace second = JobTrace.GetOrCreate(context);

        Assert.Equal(first, second);
    }
}
