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

        services.AddTransient<IRequestHandler<GetAllQueryRequest<Get_Ord_Dto>, Response<IEnumerable<Get_Ord_Dto>?>>, GetAllOrganizationHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Add_Org_Dto>, Response<long?>>, AddOrganizationHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Update_Org_Dto>, Response<long?>>, UpdateOrganizationHandler>();
        services.AddTransient<IRequestHandler<CommandRequest<Delete_Org_Dto>, Response<long?>>, DeleteOrganizationHandler>();
        //  services.AddTransient<IRequestHandler<QueryRequest<long, Get_ControllerDTO>, QResult<Get_ControllerDTO?>>, GetByIdControllerHandler>();
    }
}

