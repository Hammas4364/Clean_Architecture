using AutoWrapper.Wrappers;
using Domain.ViewModels;
using SharedKernel.Helpers;

namespace Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<ApiResponse> AddAsync(Add_Emp_Dto addEmpDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> UpdateAsync(Update_Emp_Dto UpdateEmpDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> AddOrUpdateAsync(Update_Emp_Dto UpdateEmpDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> DeleteAsync(Delete_Emp_Dto deleteEmpDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> GetAllAsync(GetAllParams getAllParams, CancellationToken cancellationToken = new());
    Task<ApiResponse> GetEmployeeByOrgAsync(GetAllParams @params, CancellationToken cancellationToken);
    Task<ApiResponse> GetAsync(long Id, CancellationToken cancellationToken = new());
}
