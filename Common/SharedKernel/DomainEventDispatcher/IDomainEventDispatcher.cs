namespace SharedKernel;
using SharedKernel.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchEvent(IEnumerable<IDomainEvent> events);
}
