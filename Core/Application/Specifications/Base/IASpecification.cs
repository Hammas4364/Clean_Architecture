namespace Application.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Entity;
using System.Linq;

public interface IASpecification<TEntity> where TEntity : class, IEntity
{
    Func<IQueryable<TEntity>, IQueryable<TEntity>> SpecificationFunc { get; }
    public IQueryable<TEntity> AsQueryable(IQueryable<TEntity> Query)
    {
        Query = Query.AsNoTracking();
        return SpecificationFunc(Query);
    }
}

public interface IASpecification<TEntity, TResult> where TEntity : class, IEntity
{
    Func<IQueryable<TEntity>, IQueryable<TResult>> SpecificationFunc { get; }
    public IQueryable<TResult> AsQueryable(IQueryable<TEntity> Query)
    {
        Query = Query.AsNoTracking();
        return SpecificationFunc(Query);
    }
}

public interface ISpecification<TEntity> where TEntity : class
{
    Func<IQueryable<TEntity>, IQueryable<TEntity>> SpecificationFunc { get; }
    IQueryable<TEntity> AsQueryable(IQueryable<TEntity> query);
}

public interface ISpecification<TEntity, TResult> where TEntity : class
{
    Func<IQueryable<TEntity>, IQueryable<TResult>> SpecificationFunc { get; }
    IQueryable<TResult> AsQueryable(IQueryable<TEntity> query);
}
