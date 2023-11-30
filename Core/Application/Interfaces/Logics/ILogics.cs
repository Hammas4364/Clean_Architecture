using Application.Interfaces.Repositories;
using Application.Specifications.Base;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;
using SharedKernel.Helpers;

namespace Application.Interfaces.Logics;

public interface ILogicsRepository
{
    Task<Response<IEnumerable<Get_EmpByOrgId_Dto>?>> GetEemployByOrganization(long? OrgId);
}
