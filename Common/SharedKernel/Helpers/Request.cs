using MediatR;
namespace SharedKernel.Helpers;

public record CommandRequest<TRequest>(TRequest Dto) : IRequest<AppResult<long?>>;

public record CommandRequest<TRequest, TResponse>(TRequest Dto) : IRequest<AppResult<TResponse?>>;

public record GetAllQueryRequest(GetAllParams GetAllParams);

public record GetAllQueryRequest<TResponse>(GetAllParams GetAllParams) : GetAllQueryRequest(GetAllParams), IRequest<AppResult<IEnumerable<TResponse>?>>;

public record QueryRequest<TRequest, TResponse>(TRequest Request) : IRequest<AppResult<TResponse?>>;

public record QueryRequest<TResponse>() : IRequest<AppResult<TResponse?>>;

public record GetAllParams(string? SearchValue = default, int? PageIndex = default, int? PageSize = default);
