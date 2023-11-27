namespace System.Linq;
using System;
using System.Linq.Expressions;

public static class SpecificationBuilderExtension
{
    public static IQueryable<T> WhereOrgId<T>(this IQueryable<T> query, long organizationId)
    {
        var parameter = Expression.Parameter(typeof(T));
        var leftExpression = Expression.PropertyOrField(parameter, "OrganizationId");
        var rightExpression = Expression.Constant(organizationId);
        var equalExpression = Expression.Equal(leftExpression, rightExpression);
        var lambdaExpression = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);
        return query.Where(lambdaExpression);
    }
    public static IQueryable<T> Where<T, TId>(this IQueryable<T> query, TId id) => query.Where("Id", id);
    public static IQueryable<T> Where<T, TValue>(this IQueryable<T> query, string columnName, TValue value)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftExpression = Expression.PropertyOrField(parameter, columnName);

        var rightExpression = Expression.Constant(value);

        var equalExpression = Expression.Equal(leftExpression, rightExpression);

        var lambdaExpression = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

        return query.Where(lambdaExpression);
    }
    public static IQueryable<T> Pagging<T>(this IQueryable<T> query, int? pageIndex, int? pageSize)
    {
        if (!pageSize.HasValue)
            return query;
        pageIndex ??= 1;
        return query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
    }
}
