using Shared.Aggregate.Domain;
using Shared.Bus.Event.Domain;

namespace Shared.Entity.Domain
{
    public abstract class IAggregateEntity : IEntity, IAggregate
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
