using Application.Specifications;
using Application.Specifications.Base;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using SharedKernel.AggregateRoot;
using SharedKernel.Exceptions;
using SharedKernel.Helpers;

namespace Application.Interfaces.Repositories;

public interface IRepositoryBase
{
    DbContext DbContext { get; }
}

public interface IReadRepository : IRepositoryBase
{
    // Without AgregatRoot
    protected virtual IQueryable<TEntity> ApplySpecification<TEntity>(IASpecification<TEntity> specification, bool evaluateCriteriaOnly = false) where TEntity : class
    {
        return specification.AsQueryable(DbContext.Set<TEntity>());
    }
    protected virtual IQueryable<TResult> ApplySpecification<TEntity, TResult>(IASpecification<TEntity, TResult> specification) where TEntity : class
    {
        return specification.AsQueryable(DbContext.Set<TEntity>());
    }
    async ValueTask<Response<TEntity?>> GetByIdAsync<TEntity, TId>(TId id, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot where TId : notnull
    {
        try
        {
            var result = await DbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> FirstOrDefaultAsync<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<TResult?>> FirstOrDefaultAsync<TEntity, TResult>(IASpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> SingleOrDefaultAsync<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TResult?>> SingleOrDefaultAsync<TEntity, TResult>(IASpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);

            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> GetAllAsync<TEntity>(CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await DbContext.Set<TEntity>().ToListAsync(cancellationToken);

            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> GetAllAsync<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification).ToListAsync(cancellationToken);

            //    return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<IEnumerable<TResult>?>> GetAllAsync<TEntity, TResult>(IASpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification).ToListAsync(cancellationToken);

            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<int?>> CountAsync<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default) where TEntity : class
    {
        try
        {
            return await ApplySpecification(specification, true).CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<int?>> CountAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
    {
        try
        {
            return await DbContext.Set<TEntity>().CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<bool?>> AnyAsync<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await ApplySpecification(specification, true).AnyAsync(cancellationToken);
            if (throwOnNotFoundException && result is false)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is true)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<bool?>> AnyAsync<TEntity>(CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class
    {
        try
        {
            var result = await DbContext.Set<TEntity>().AnyAsync(cancellationToken);
            if (throwOnNotFoundException && result is false)
                return RepositoryExceptions.NotFoundException<TEntity>();

            if (throwOnAlreadyExist && result is true)
                return RepositoryExceptions.AlreadyExistException<TEntity>();

            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }


    // With AgregatRoot
    protected virtual IQueryable<TEntity> ApplySpecification1<TEntity>(IASpecification<TEntity> specification, bool evaluateCriteriaOnly = false) where TEntity : class, IAggregateRoot
    {
        return specification.AsQueryable(DbContext.Set<TEntity>());
    }
    protected virtual IQueryable<TResult> ApplySpecification1<TEntity, TResult>(IASpecification<TEntity, TResult> specification) where TEntity : class, IAggregateRoot
    {
        return specification.AsQueryable(DbContext.Set<TEntity>());
    }
    async ValueTask<Response<TEntity?>> GetByIdAsync1<TEntity, TId>(TId id, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot where TId : notnull
    {
        try
        {
            var result = await DbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> FirstOrDefaultAsync1<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification).FirstOrDefaultAsync(cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<TResult?>> FirstOrDefaultAsync1<TEntity, TResult>(IASpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification).FirstOrDefaultAsync(cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> SingleOrDefaultAsync1<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification).SingleOrDefaultAsync(cancellationToken);
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TResult?>> SingleOrDefaultAsync1<TEntity, TResult>(IASpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification).SingleOrDefaultAsync(cancellationToken);

            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> GetAllAsync1<TEntity>(CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await DbContext.Set<TEntity>().ToListAsync(cancellationToken);

            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> GetAllAsync1<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification).ToListAsync(cancellationToken);

            //    return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<IEnumerable<TResult>?>> GetAllAsync1<TEntity, TResult>(IASpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification).ToListAsync(cancellationToken);

            if (throwOnNotFoundException && result is null)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is not null)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<int?>> CountAsync1<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default) where TEntity : class, IAggregateRoot
    {
        try
        {
            return await ApplySpecification1(specification, true).CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<int?>> CountAsync1<TEntity>(CancellationToken cancellationToken = default) where TEntity : class, IAggregateRoot
    {
        try
        {
            return await DbContext.Set<TEntity>().CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<bool?>> AnyAsync1<TEntity>(IASpecification<TEntity> specification, CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await ApplySpecification1(specification, true).AnyAsync(cancellationToken);
            if (throwOnNotFoundException && result is false)
                return RepositoryExceptions.NotFoundException<TEntity>();
            if (throwOnAlreadyExist && result is true)
                return RepositoryExceptions.AlreadyExistException<TEntity>();
            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
    async Task<Response<bool?>> AnyAsync1<TEntity>(CancellationToken cancellationToken = default, bool throwOnNotFoundException = true, bool throwOnAlreadyExist = false) where TEntity : class, IAggregateRoot
    {
        try
        {
            var result = await DbContext.Set<TEntity>().AnyAsync(cancellationToken);
            if (throwOnNotFoundException && result is false)
                return RepositoryExceptions.NotFoundException<TEntity>();

            if (throwOnAlreadyExist && result is true)
                return RepositoryExceptions.AlreadyExistException<TEntity>();

            return result;
        }
        catch (Exception ex)
        {

            return ex;
        }
    }
}

public interface IWriteRepository : IRepositoryBase
{
    // Without AgregatRoot
    async Task<Response<TEntity?>> EnableChangeTracker<TEntity>(TEntity entity) where TEntity : class
    {
        try
        {
            DbContext.Attach(entity);
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().Add(entity);
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);

                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().AddRange(entities);
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);

                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return await Task.FromResult(ResponseResult.From(entities));
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().Update(entity);

            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);

                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().UpdateRange(entities);
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);
                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return await Task.FromResult(ResponseResult.From(entities));
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<int?>> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }


    // With AgregatRoot

    async Task<Response<TEntity?>> EnableChangeTracker1<TEntity>(TEntity entity) where TEntity : class, IAggregateRoot
    {
        try
        {
            DbContext.Attach(entity);
            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> AddAsync1<TEntity>(TEntity entity, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class, IAggregateRoot
    {
        try
        {
            DbContext.Set<TEntity>().Add(entity);
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);

                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> AddRangeAsync1<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class, IAggregateRoot
    {
        try
        {
            DbContext.Set<TEntity>().AddRange(entities);
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);

                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return await Task.FromResult(ResponseResult.From(entities));
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> UpdateAsync1<TEntity>(TEntity entity, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class, IAggregateRoot
    {
        try
        {
            DbContext.Set<TEntity>().Update(entity);

            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);

                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> UpdateRangeAsync1<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class, IAggregateRoot
    {
        try
        {
            DbContext.Set<TEntity>().UpdateRange(entities);
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);
                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return await Task.FromResult(ResponseResult.From(entities));
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<TEntity?>> DeleteAsync1<TEntity>(IDeleted<TEntity> deletedEntity1, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class, IAggregateRoot
    {
        try
        {
            DbContext.Entry(deletedEntity1.Entity).State = EntityState.Deleted;
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);
                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return deletedEntity1.Entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
    async Task<Response<IEnumerable<TEntity>?>> DeleteRangeAsync1<TEntity>(IEnumerable<IDeleted<TEntity>> deletedEntities1, CancellationToken cancellationToken = default, bool autoSave = true) where TEntity : class, IAggregateRoot
    {
        try
        {
            var entities = deletedEntities1.Select(_ => _.Entity);
            DbContext.Set<TEntity>().RemoveRange();
            if (autoSave)
            {
                var SaveChangesResult = await SaveChangesAsync(cancellationToken);
                if (SaveChangesResult.Status is Status.Exception)
                    return SaveChangesResult.Exception!;
            }
            return await Task.FromResult(ResponseResult.From(entities));
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}

public interface IRepository : IReadRepository, IWriteRepository
{
    public virtual async Task<Response<TEntity?>> DeleteIfAnyElseThrowAsync<TEntity, TId>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IAggregateRoot<TId>
    {
        try
        {
            var hasAnyEntityResult = await AnyAsync1(Specs.Common.GetById<TEntity, TId>(entity.Id), cancellationToken);
            if (hasAnyEntityResult.Status is Status.Exception)
                return hasAnyEntityResult.Exception!;
            DbContext.Entry(entity).State = EntityState.Deleted;
            var SaveChangesResult = await SaveChangesAsync(cancellationToken);
            if (SaveChangesResult.Status is Status.Exception)
                return SaveChangesResult.Exception!;
            return entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}