namespace Infrastructure;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        //REPOSITORIES
        services.AddScoped(typeof(IRepository), typeof(Repository));
        services.AddTransient<IDapperRepository, DapperRepository>();

        // SERVICES
        services.AddScoped<IOrganizationService, OrganizationService>();
    }
}
