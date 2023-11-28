using SharedKernel.Interfaces;
namespace SharedKernel.Entity;

public abstract record Entity : IEntity
{
    protected Entity() { }
    public DateTime? CreatedDate { get; private set; }
    public DateTime? LastModifiedDate { get; private set; }

}


public abstract record Entity<TId> : Entity
{
    protected Entity() { }

    public TId Id { get; set; } = default!;
};
