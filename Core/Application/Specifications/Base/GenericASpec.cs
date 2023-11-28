namespace Application.Specifications.Base;
using System;
using System.Data;
using System.Linq;
using SharedKernel.Entity;

internal class GenericASpec<T> : IASpecification<T> where T : class
{
    public Func<IQueryable<T>, IQueryable<T>> SpecificationFunc { get; init; } = default!;
}
internal class GenericASpec<T, TResponse> : IASpecification<T, TResponse> where T : class
{
    public Func<IQueryable<T>, IQueryable<TResponse>> SpecificationFunc { get; init; } = default!;
}
public class GenericDSpec<T> : IDSpecification<T>
{
    public string CommandText { get; init; } = default!;
    public object Parameters { get; init; } = default!;
    public CommandType CommandType { get; init; } = CommandType.StoredProcedure;
}
