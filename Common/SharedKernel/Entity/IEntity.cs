namespace SharedKernel.Entity;

public interface IEntity
{
}

public interface IEntity<TId> : IEntity
{
    public TId Id { get; }
}