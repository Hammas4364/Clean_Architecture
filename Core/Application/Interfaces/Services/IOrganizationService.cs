namespace Application.Interfaces.Services;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using SharedKernel.Helpers;

public interface IOrganizationService
{
    Task<ApiResponse> AddAsync(Add_Org_Dto addUpdateOrgDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> UpdateAsync(Update_Org_Dto addUpdateOrgDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> DeleteAsync(Delete_Org_Dto delete_OrgDTO, CancellationToken cancellationToken = new());
    Task<ApiResponse> GetAllAsync(GetAllParams getAllParams, CancellationToken cancellationToken = new());
    Task<ApiResponse> GetAsync(long Id, CancellationToken cancellationToken = new());
}
