namespace Infrastructure.Repositories;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

public class Repository : IRepository
{
    public DbContext DbContext { get; }
    public Repository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        DbContext = dbContextFactory.CreateDbContext();
    }
}
