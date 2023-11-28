namespace Application.Specifications;
using Application.Specifications.Base;
using Dapper;
using Humanizer;
using SharedKernel.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

internal static partial class DSpecs
{
    internal static class Common
    {
        internal static GenericDSpec<TEntity> DeleteSpec<TEntity, TId>(TEntity entity) where TEntity : class
        {
            return Get_DeleteCommandDefinitation<TEntity>($"[Id]={entity}");
        }

        private static GenericDSpec<TEntity> Get_DeleteCommandDefinitation<TEntity>(string condition) where TEntity : class
        {
            var param = new DynamicParameters();
            param.Add("@tableName", typeof(TEntity).Name.Pluralize());
            param.Add("@condition", condition);
            param.Add("@id", direction: System.Data.ParameterDirection.Output);
            param.Add("@return_Message", direction: System.Data.ParameterDirection.Output);

            return new GenericDSpec<TEntity>()
            {
                CommandText = "g_Delete_ByCondition",
                Parameters = param
            };
        }

        internal static GenericASpec<TEntity, TResponse> GetById<TEntity, TResponse>(long id, Expression<Func<TEntity, TResponse>> selectExpression) where TEntity : class
        => new()
        {
            SpecificationFunc = _ => _.Where(id).Select(selectExpression)
        };
              
        internal static GenericASpec<TEntity> GetByColumn<TEntity>(string columnName, object value) where TEntity : class
        => new()
        {
            SpecificationFunc = _ => _.Where(columnName, value)
        };

        internal static GenericASpec<TEntity, TResponse> GetById<TEntity, TResponse, TValue>(string columnName, TValue value, Expression<Func<TEntity, TResponse>> selectExpression) where TEntity : class
        => new()
        {
            SpecificationFunc = _ => _.Where(columnName, value).Select(selectExpression)
        };
    }
}


