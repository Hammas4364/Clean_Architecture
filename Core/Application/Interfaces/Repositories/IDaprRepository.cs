using Application.Specifications.Base;
using Dapper;
using Humanizer;
using SharedKernel.Entity;
using SharedKernel.Exceptions;
using SharedKernel.Helpers;
using System.Data.Common;
namespace Application.Interfaces.Repositories;

public interface IDapperRepository
{
    DbConnection GetConnection();
    protected virtual CommandDefinition ApplySpecification<TEntity>(IDSpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return specification.AsCommandDefination(cancellationToken);
    }
   
    public async Task<Response<IEnumerable<TEntity>?>> QueryAsync<TEntity>(IDSpecification<TEntity> specification, bool throwOnNotFoundException = true, CancellationToken cancellationToken = default)
    {
        using var con = GetConnection();
        con.Open();
        var result = await con.QueryAsync<TEntity>(ApplySpecification(specification, cancellationToken));
        if (throwOnNotFoundException && (result is null || !result.Any()))
            return RepositoryExceptions.NotFoundException<TEntity>();
        return ResponseResult.From(result);
    }

    public async Task<Response<TEntity?>> QueryFirstOrDefaultAsync<TEntity>(IDSpecification<TEntity> specification, bool throwOnNotFoundException = true, CancellationToken cancellationToken = default)
    {
        using var con = GetConnection();
        var result = await con.QueryFirstOrDefaultAsync<TEntity>(ApplySpecification(specification, cancellationToken));
        if (throwOnNotFoundException && result is null)
            return RepositoryExceptions.NotFoundException<TEntity>();
        return result;
    }

    public async Task<Response<TEntity?>> QuerySingleOrDefaultAsync<TEntity>(IDSpecification<TEntity> specification, bool throwOnNotFoundException = true, CancellationToken cancellationToken = default)
    {
        using var con = GetConnection();
        con.Open();
        var result = await con.QuerySingleOrDefaultAsync<TEntity>(ApplySpecification(specification, cancellationToken));
        if (throwOnNotFoundException && result is null)
            return RepositoryExceptions.NotFoundException<TEntity>();
        return result;
    }

    public async Task<Response<SqlMapper.GridReader?>> QueryMultipleAsync<TEntity>(IDSpecification<TEntity> specification, bool throwOnNotFoundException = true, CancellationToken cancellationToken = default)
    {
        using var con = GetConnection();
        con.Open();
        var result = await con.QueryMultipleAsync(ApplySpecification(specification, cancellationToken));
        if (throwOnNotFoundException && result is null)
            return RepositoryExceptions.NotFoundException<TEntity>();
        return result;
    }

    public async Task<Response<TEntity?>> ExecuteScalarAsync<TEntity>(IDSpecification<TEntity> specification, bool throwOnNotFoundException = true, CancellationToken cancellationToken = default)
    {
        using var con = GetConnection();
        con.Open();
        var result = await con.ExecuteScalarAsync<TEntity>(ApplySpecification(specification, cancellationToken));
        if (throwOnNotFoundException && result is null)
            return RepositoryExceptions.NotFoundException<TEntity>();
        return result;
    }

    public async Task<Response<TId?>> DeleteAsync<TEntity, TId>(IDSpecification<TEntity> specification, bool validateBeforeExecution = true, CancellationToken cancellationToken = default) where TEntity : class, IEntity<TId>
    {
        using var con = GetConnection();
        con.Open();
        await con.ExecuteAsync(ApplySpecification(specification, cancellationToken));
        var pram = specification.Parameters as DynamicParameters;
        if (validateBeforeExecution)
        {
            var message_Response = pram!.Get<string>("@return_Message");
            if (message_Response is not "OK")
            {
                var entity = message_Response.Split('.');
                entity[0] = entity[0].Singularize();
                throw new AppException(string.Join('.', entity).Singularize());
            }
        }
        return pram!.Get<TId>("@id");
    }
}
