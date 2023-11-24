using MediatR;
using SharedKernel.Helpers;
namespace SharedKernel.Sender;

public record Sender(IMediator Mediator) : ISender
{
    public Task<AppResult<long?>> Send<TRequest>(CommandRequest<TRequest> request, CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }

    public Task<AppResult<TResponse?>> Send<TRequest, TResponse>(CommandRequest<TRequest, TResponse> request, CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }

    public Task<AppResult<TResponse?>> Send<TRequest, TResponse>(QueryRequest<TRequest, TResponse> request, CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }

    public Task<AppResult<IEnumerable<TResponse>?>> Send<TResponse>(GetAllQueryRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }

    public Task<AppResult<TResponse?>> Send<TResponse>(QueryRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return Mediator.Send(request, cancellationToken);
    }
}