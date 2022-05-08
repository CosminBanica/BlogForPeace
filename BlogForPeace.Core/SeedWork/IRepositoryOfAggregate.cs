namespace BlogForPeace.Core.SeedWork
{
    public interface IRepositoryOfAggregate<TAggregate, TAggregateCreateCommand>
        where TAggregate : Entity, IAggregateRoot
        where TAggregateCreateCommand : ICreateAggregateCommand
    {
        Task AddAsync(TAggregateCreateCommand command, CancellationToken cancellationToken);
        Task<DomainOfAggregate<TAggregate>?> GetAsync(int aggregateId, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
