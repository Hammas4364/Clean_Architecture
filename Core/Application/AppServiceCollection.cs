namespace Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using SharedKernel.Helpers;
using SharedKernel.Claims;
using Application.Common.Sender;
using System;
using MediatR;
using Application.Common.DBConnection;
using Domain.ViewModels;
using Application.Handlers.Organization;
using Application.Handlers;
using Domain.Models;
using Application.Handlers.Employee;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringsOption>(configuration.GetSection(ConnectionStringsOption.SectionName));
        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<IClaims, Claims>();
        services.AddScoped<IASender, ASender>();
        services.AddScoped<IDBConnection, DBConnection>();

        // Organization Hanlders
        services.AddTransient<IRequestHandler<GetAllQueryRequest<Get_Org_Dto>, Response<IEnumerable<Get_Org_Dto>?>>, GetAllOrganizationHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Add_Org_Dto>, Response<long?>>, AddOrganizationHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Update_Org_Dto>, Response<long?>>, UpdateOrganizationHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Delete_Org_Dto>, Response<long?>>, DeleteOrganizationHandler>();
        services.AddTransient<IRequestHandler<QueryRequest<long, Get_OrgById_Dto>, Response<Get_OrgById_Dto?>>, GetOrganizationByIdHandler>();

        // Employee Hanlders
        services.AddTransient<IRequestHandler<GetAllQueryRequest<Emp_Dto>, Response<IEnumerable<Emp_Dto>?>>, GetAllEmployeeHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Add_Emp_Dto>, Response<long?>>, AddEmployeeHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Update_Emp_Dto>, Response<long?>>, UpdateEmployeeHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Delete_Emp_Dto>, Response<long?>>, DeleteEmployeeHandler>();
        services.AddTransient<IRequestHandler<QueryRequest<long, Emp_Dto>, Response<Emp_Dto?>>, GetEmployeeByIdHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Update_Emp_Dto>, Response<long?>>, AddOrUpdateEmployeeHandler>();
        //services.AddTransient<IRequestHandler<GetAllQueryRequest<Get_EmpByOrgId_Dto>, Response<IEnumerable<Get_EmpByOrgId_Dto>?>>, GetEmployeeByOrgHandler>();
    }
}

