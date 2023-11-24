using SharedKernel.Entity;
using SharedKernel.Interfaces;

namespace SharedKernel.AggregateRoot;

public interface IAggregateRoot : IEntity
{
    public string AggregateId => GetType().Name;
    public List<IDomainEvent> DomainEvents { get; }
    public IEnumerable<IDomainEvent> GetDomainEvents() => DomainEvents.AsReadOnly();

    public void ClearDomainEvents() => DomainEvents.Clear();
}


public interface IAggregateRoot<TId> : IEntity<TId>, IAggregateRoot
{

}
