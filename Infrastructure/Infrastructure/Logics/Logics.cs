using Application.Interfaces.Logics;
using Domain.ViewModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Helpers;
namespace Infrastructure.Logics;

public class LogicsRepository : ILogicsRepository
{
    private readonly AppDbContext db;
    public LogicsRepository(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<Response<IEnumerable<Get_EmpByOrgId_Dto>?>> GetEemployByOrganization(long? OrgId)
    {
        try
        {
            var result = await (from e in db.Employees
                              join o in db.Orgainzations on e.OrgId equals o.Id
                              select new Get_EmpByOrgId_Dto
                              {
                                  Id = e.Id,
                                  OrgId = e.OrgId,
                                  EmployeeCode = e.EmployeeCode,
                                  EmployeeName = e.EmployeeName,
                                  OrgName = o.OrgName,
                                  Active = e.Active,
                                  CreatedDate = e.CreatedDate,
                                  ModifiedDate = e.ModifiedDate
                              }).ToListAsync();
            if (result is null)
                return RepositoryExceptions.NotFoundException<Get_EmpByOrgId_Dto>();
            if (result is not null)
                return RepositoryExceptions.AlreadyExistException<Get_EmpByOrgId_Dto>();
            return result;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
