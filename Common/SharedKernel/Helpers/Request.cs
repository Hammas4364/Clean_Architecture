using MediatR;
namespace SharedKernel.Helpers;

public record CommandRequest<TRequest>(TRequest Dto) : IRequest<Response<long?>>;

public record CommandRequest<TRequest, TResponse>(TRequest Dto) : IRequest<Response<TResponse?>>;

public record GetAllQueryRequest(GetAllParams GetAllParams);
public record GetAllQueryRequest<TResponse>(GetAllParams GetAllParams) : GetAllQueryRequest(GetAllParams), IRequest<Response<IEnumerable<TResponse>?>>;

public record QueryRequest<TRequest, TResponse>(TRequest Request) : IRequest<Response<TResponse?>>;

public record QueryRequest<TResponse>() : IRequest<Response<TResponse?>>;

public record GetAllParams(string? SearchValue = default, int? PageIndex = default, int? PageSize = default);
