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
    public DbSet<Employee> Employees => Set<Employee>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(TimeSpan.FromMinutes(1));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => new { e.Id });
            entity.ToTable("Organizations");
            entity.Property(e => e.OrgName).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.OrgDetail).HasMaxLength(200).IsUnicode(false);
            entity.Property(e => e.Active).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => new { e.Id });
            entity.ToTable("Employees");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.EmployeeName).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.OrgId).HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Active).HasMaxLength(1).IsUnicode(false).IsFixedLength();
        });
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
