namespace Application.Specifications;
using Application.Specifications.Base;
using Domain.Models;
using SharedKernel.Entity;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Helpers;
using Domain.ViewModels;

internal static partial class Specs
{
    internal static class Common
    {
        internal static GenericASpec<TEntity> GetById<TEntity, TId>(TId id) where TEntity : class
        => new()
        {
            SpecificationFunc = _ => _.Where(id)
        };

        internal static GenericASpec<TEntity, TResponse> GetById<TEntity, TResponse>(long id, Expression<Func<TEntity, TResponse>> selectExpression) where TEntity : class
        => new()
        {
            SpecificationFunc = _ => _.Where(id).Select(selectExpression)
        };

        internal static GenericASpec<TEntity> GetByIdWithInclude<TEntity, TIncludeProperty>(long id, Expression<Func<TEntity, IEnumerable<TIncludeProperty>>> includeExpression) where TEntity : class, IEntity => new()
        {
            SpecificationFunc = _ => _.Include(includeExpression).Where(id)
        };

        internal static GenericASpec<TEntity> GetByColumn<TEntity>(string columnName, object value) where TEntity : class
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

    internal static class OrganizationSpecs
    {
        public static GenericASpec<Organization> CheckNameAlreadyExists(long Id, string Name) => new()
        {
            SpecificationFunc = _ =>
            _.Where(_ => _.OrgName == Name && _.Id != Id)
        };
    }

    internal static class EmployeeSpecs
    {
        public static GenericASpec<Employee> CheckEmployeeAlreadyExists(long OrgId, long EmployeeCode) => new()
        {
            SpecificationFunc = _ =>
            _.Where(_ => _.OrgId == OrgId && _.EmployeeCode != EmployeeCode)
        };

        public static GetAllSpec<Employee, Emp_Dto> GetAllEmployeeSpecs(GetAllParams getAllParams)
        {
            return new GetAllSpec<Employee, Emp_Dto>()
            {
                SearchValue = getAllParams.SearchValue,
                SearchExpression = _ => getAllParams.SearchValue != null ? _.EmployeeName!.ToLower().Contains(getAllParams.SearchValue!.ToLower()) : _.OrgId == -1,
                PageSize = getAllParams.PageSize,
                PageNumber = getAllParams.PageIndex,
                SelectExpression = _ => new Emp_Dto 
                { 
                    Id = _.Id, 
                    OrgId = _.OrgId,
                    EmployeeName = _.EmployeeName, 
                    EmployeeCode = _.EmployeeCode, 
                    Active = _.Active, 
                    CreatedDate = _.CreatedDate, 
                    ModifiedDate = _.ModifiedDate, 
                },
                OrderBy = _ => _.EmployeeName!,
            };
        }
    }
}
