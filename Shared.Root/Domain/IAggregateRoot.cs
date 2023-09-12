using Shared.Aggregate.Domain;
using Shared.Bus.Event.Domain;

namespace Shared.Root.Domain
{
    public abstract class IAggregateRoot : IRoot, IAggregate
    {
        private List<IEvent> AggregateEvents { get; set; } = new List<IEvent>();

        public void AddAggregateEvent(IEvent aggregate)
        {
            AggregateEvents.Add(aggregate);
        }

        public void AddAggregateEvent(IEnumerable<IEvent> aggregate)
        {
            AggregateEvents.AddRange(aggregate.ToList());
        }

        public List<IEvent> PullAggregateEvents()
        {
            List<IEvent> AggregateEventsAux = AggregateEvents;
            AggregateEvents = new List<IEvent>();

            return AggregateEventsAux;
        }
    }
}
