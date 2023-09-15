﻿using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain
{
    public abstract class IAggregateMessage : IMessage, IAggregate
    {
        public Guid AggregateId { get; }
        public string AggregateName { get; }
        public DateTime AggregateOccurredAt { get; }

        protected IAggregateMessage(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt)
        {
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            AggregateOccurredAt = aggregateOccurredAt;
        }

        public static string AggregateRoute() => throw new NotImplementedException();
    }
}
