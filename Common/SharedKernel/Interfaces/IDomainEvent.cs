using MediatR;
namespace SharedKernel.Interfaces;

public interface IDomainEvent : INotification
{
}

public interface IDeleteDomainEvent : IDomainEvent
{
}
