//namespace SharedKernel.AggregateRoot;
//using System.Collections.Generic;
//using SharedKernel.Entity;
//using SharedKernel.Interfaces;

//public abstract record AggregateRoot : Entity, IAggregateRoot
//{
//    protected AggregateRoot() : base() { }
//    public List<IDomainEvent> DomainEvents => _domainEvents;
//}

//public abstract record AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
//{
//    protected AggregateRoot() : base()
//    {
//    }

//    public List<IDomainEvent> DomainEvents => _domainEvents;
//}
