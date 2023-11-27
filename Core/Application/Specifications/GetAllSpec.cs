namespace Application.Specifications;

using System.Linq;
using System.Linq.Expressions;
using Application.Specifications.Base;
using SharedKernel.Entity;
using Microsoft.EntityFrameworkCore;

internal record GetAllSpec
{
    public int? PageSize { get; init; } = default!;
    public int? PageNumber { get; init; } = null!;
    public string? SearchValue { get; init; } = null!;
}

internal record GetAllSpec<TEntity> : GetAllSpec, IASpecification<TEntity> where TEntity : class, IEntity
{
    public Expression<Func<TEntity, bool>>? SearchExpression { get; init; } = default!;
    Func<IQueryable<TEntity>, IQueryable<TEntity>> IASpecification<TEntity>.SpecificationFunc => query =>
    {
        if (SearchValue is not null && SearchExpression is not null)
            query = query.Where(SearchExpression);
        return query.Pagging(PageNumber, PageSize);
    };
}

internal record GetAllSpec<TEntity, TResult> : GetAllSpec, IASpecification<TEntity, TResult> where TEntity : class, IEntity
{
    public bool IgnoreQueryFilter { get; set; } = false;
    public Func<IQueryable<TEntity>, IQueryable<TResult>> SpecificationFunc => query =>
    {
        if (WhereExpression is not null)
            query = query.Where(WhereExpression);

        if (SearchValue is not null && SearchExpression is not null)
            query = query.Where(SearchExpression);

        query = query.Pagging(PageNumber, PageSize);

        if (OrderByDescending is not null)
            query = query.OrderByDescending(OrderByDescending);
        if (OrderBy is not null)
            query = query.OrderBy(OrderBy);

        if (IgnoreQueryFilter)
            query = query.IgnoreQueryFilters();
        return query.Select(SelectExpression);
    };
    public Expression<Func<TEntity, TResult>> SelectExpression { get; init; } = default!;
    public Expression<Func<TEntity, bool>>? SearchExpression { get; init; } = default!;
    public Expression<Func<TEntity, bool>>? WhereExpression { get; init; } = default!;
    public Expression<Func<TEntity, object>> OrderByDescending { get; init; } = default!;
    public Expression<Func<TEntity, object>> OrderBy { get; init; } = default!;
}