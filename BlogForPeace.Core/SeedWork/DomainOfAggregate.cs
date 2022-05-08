namespace BlogForPeace.Core.SeedWork
{
    public abstract class DomainOfAggregate<TAggregate> where TAggregate : Entity, IAggregateRoot
    {
        private protected readonly TAggregate aggregate;

        public DomainOfAggregate(TAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
    }
}
