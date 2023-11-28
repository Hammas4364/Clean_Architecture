namespace Application.Common;
using SharedKernel.Entity;
using SharedKernel.Interfaces;

public interface IDeleted<T> where T : IEntity
{
    T Entity { get; }
}
public class Deleted<T> : IDeleted<T> where T : Entity
{
    public Deleted(T model, IDeleteDomainEvent deleteEvent)
    {
        DeleteEvent = deleteEvent;
        Entity = model;
        Entity.RegisterEvent(deleteEvent);
    }

    public IDeleteDomainEvent DeleteEvent { get; }
    public T Entity { get; }
}
