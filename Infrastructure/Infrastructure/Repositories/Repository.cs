namespace Infrastructure.Repositories;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System.Threading.Tasks;
using SharedKernel.Helpers;
using System.Collections.Generic;
using Domain.ViewModels;
using SharedKernel.Exceptions;

public class Repository : IRepository
{
    public DbContext DbContext { get; }
    public Repository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        DbContext = dbContextFactory.CreateDbContext();
    }
    
}
