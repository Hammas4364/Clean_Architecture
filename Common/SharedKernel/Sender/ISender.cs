using MediatR;
using SharedKernel.Helpers;
namespace SharedKernel.Sender;

public interface ISender
{
    IMediator Mediator { get; }
    Task<AppResult<long?>> Send<TRequest>(CommandRequest<TRequest> request, CancellationToken cancellationToken = default);
    Task<AppResult<TResponse?>> Send<TRequest, TResponse>(CommandRequest<TRequest, TResponse> request, CancellationToken cancellationToken = default);
    Task<AppResult<IEnumerable<TResponse>?>> Send<TResponse>(GetAllQueryRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task<AppResult<TResponse?>> Send<TRequest, TResponse>(QueryRequest<TRequest, TResponse> request, CancellationToken cancellationToken = default);
    Task<AppResult<TResponse?>> Send<TResponse>(QueryRequest<TResponse> request, CancellationToken cancellationToken = default);
}