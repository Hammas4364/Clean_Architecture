﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Serilog;
using SharedKernel.Claims;
using Persistence.Data.Config;
using Domain.Models;
namespace Persistence.Data;

public class AppDbContext : DbContext
{
    public DbSet<Orgainzation> Orgainzations => Set<Orgainzation>();

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