namespace Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Persistence.Data.Config;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Design;
using SharedKernel.Constants;

public class AppDbContext : DbContext
{
    public DbSet<Organization> Orgainzations => Set<Organization>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(TimeSpan.FromMinutes(1));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrgainzationConfig());
        base.OnModelCreating(modelBuilder);
    }

    ~AppDbContext()
    {
        Log.Verbose("Disposing AppDbContext");
        Dispose();
    }
    public override void Dispose()
    {
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(ConnectionString.cs);
        return new AppDbContext(optionsBuilder.Options);
    }
}
