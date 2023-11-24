using SharedKernel.Interfaces;
namespace SharedKernel.Entity;

public abstract record Entity : IEntity
{
    protected readonly List<IDomainEvent> _domainEvents = new();

    public void RegisterEvent(IDomainEvent domainEvent)
    {
        When(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    protected void RegisterEvents(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            When(domainEvent);
        }
        _domainEvents.AddRange(domainEvents);
    }

    protected abstract void When(IDomainEvent @event);
    protected Entity() { }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? LastModifiedDate { get; private set; }
}

public abstract record Entity<TId> : Entity, IEntity<TId>
{
    protected Entity() { }
    public TId Id { get; set; } = default!;
};