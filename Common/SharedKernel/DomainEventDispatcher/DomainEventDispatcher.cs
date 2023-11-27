namespace SharedKernel;
using MediatR;
using SharedKernel.Interfaces;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchEvent(IEnumerable<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
