namespace Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Serilog;
using SharedKernel.Claims;
using Persistence.Data.Config;
using Domain.Models;

public class AppDbContext : DbContext
{
    public DbSet<Organization> Orgainzations => Set<Organization>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(TimeSpan.FromMinutes(1));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this.GetService<IClaims>());
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
