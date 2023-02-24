using Shared.Aggregate.Domain;
using Shared.Bus.Event.Domain;

namespace Shared.Aggregate.Root.Domain
{
    public abstract class IAggregateRoot : IAggregate
    {
        private List<IEvent> AggregateEvents { get; set; } = new List<IEvent>();

        public void AddAggregateEvent(IEvent aggregate)
        {
            AggregateEvents.Add(aggregate);
        }

        public void AddAggregateEvent(List<IEvent> aggregate)
        {
            AggregateEvents.AddRange(aggregate);
        }

        public List<IEvent> PullAggregateEvents()
        {
            List<IEvent> AggregateEventsAux = AggregateEvents;
            AggregateEvents = new List<IEvent>();

            return AggregateEventsAux;
        }
    }
}
