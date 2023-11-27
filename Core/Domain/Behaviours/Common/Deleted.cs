using SharedKernel.Entity;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Behaviours.Common;
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
