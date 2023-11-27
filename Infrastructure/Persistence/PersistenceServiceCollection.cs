namespace Persistence;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Interceptors;
using SharedKernel;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtension
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<AuditOnSavingChanges>();

        services.AddPooledDbContextFactory<AppDbContext>((s, o) =>
        {
            using var scope = s.CreateScope();
            var auditOnSaveChanges = scope.ServiceProvider.GetService<AuditOnSavingChanges>();
            //if (auditOnSaveChanges is not null)
            //    o.AddInterceptors(auditOnSaveChanges);
            o.ConfigureWarnings(b =>
            {
                b.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
            });
            o.AddInterceptors(new LogCommandInterceptor());

            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!, b =>
            {
                //   b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                b.MigrationsAssembly("Persistence");
            });
        });
    }
}
