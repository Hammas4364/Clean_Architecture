namespace Application.Interfaces;
using MediatR;
using SharedKernel.Helpers;

internal interface ICommandHandler<TRequest> : IRequestHandler<CommandRequest<TRequest>, Response<long?>>
{
}

internal interface ICommandHandler<TRequest, TResponse> : IRequestHandler<CommandRequest<TRequest, TResponse>, Response<TResponse?>>
{
}

internal interface IGetAllQueryHandler<TResponse> : IRequestHandler<GetAllQueryRequest<TResponse>, Response<IEnumerable<TResponse>?>>
{
}

internal interface IQueryHandler<TRequest, TResponse> : IRequestHandler<QueryRequest<TRequest, TResponse>, Response<TResponse?>>
{
}

internal interface IQueryHandler<TResponse> : IRequestHandler<QueryRequest<TResponse>, Response<TResponse?>>
{
}
