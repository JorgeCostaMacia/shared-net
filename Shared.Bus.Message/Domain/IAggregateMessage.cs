using System.Collections.Generic;
using System;

namespace Shared.Bus.Message.Domain;

public abstract record IAggregateMessage : IMessage
{
    public Guid AggregateId { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt)
    {
        AggregateId = aggregateId;
        AggregateOccurredAt = aggregateOccurredAt;
    }

#if NET9_0
    protected IAggregateMessage() : this(Guid.CreateVersion7(), DateTime.UtcNow) { }
#else
    protected IAggregateMessage() : this(Guid.NewGuid(), DateTime.UtcNow) { }
#endif

}