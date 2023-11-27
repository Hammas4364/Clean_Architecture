namespace Application.Specifications;
using Application.Specifications.Base;
using Domain.Models;
using SharedKernel.Entity;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

internal static partial class Specs
{
    internal static class Common
    {
        internal static GenericASpec<TEntity> GetById<TEntity, TId>(TId id) where TEntity : class, IEntity
        => new()
        {
            SpecificationFunc = _ => _.Where(id)
        };

        internal static GenericASpec<TEntity, TResponse> GetById<TEntity, TResponse>(long id, Expression<Func<TEntity, TResponse>> selectExpression) where TEntity : class, IEntity
        => new()
        {
            SpecificationFunc = _ => _.Where(id).Select(selectExpression)
        };

        internal static GenericASpec<TEntity> GetByIdWithInclude<TEntity, TIncludeProperty>(long id, Expression<Func<TEntity, IEnumerable<TIncludeProperty>>> includeExpression) where TEntity : class, IEntity => new()
        {
            SpecificationFunc = _ => _.Include(includeExpression).Where(id)
        };
        internal static GenericASpec<TEntity> GetByColumn<TEntity>(string columnName, object value) where TEntity : class, IEntity
        => new()
        {
            SpecificationFunc = _ => _.Where(columnName, value)
        };

        internal static GenericASpec<TEntity, TResponse> GetById<TEntity, TResponse, TValue>(string columnName, TValue value, Expression<Func<TEntity, TResponse>> selectExpression) where TEntity : class, IEntity
        => new()
        {
            SpecificationFunc = _ => _.Where(columnName, value).Select(selectExpression)
        };
    }

    public static class OrgSpecs
    {
        public static GenericASpec<Organization> CheckNameAlreadyExists(long Id, string Name) => new()
        {
            SpecificationFunc = _ => _.Where(_ => _.OrgName == Name && _.Id != Id)
        };
    }
}
