namespace Infrastructure;
using Application.Interfaces.Logics;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Logics;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;


public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {

        //REPOSITORIES
        services.AddScoped(typeof(IRepository), typeof(Repository));
        services.AddTransient<IDapperRepository, DapperRepository>();

        // SERVICES
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
    }
}
