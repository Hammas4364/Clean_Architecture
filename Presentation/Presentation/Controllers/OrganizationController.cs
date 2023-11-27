namespace Presentation.Controllers;

using Application.Interfaces.Services;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Helpers;

public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        this.organizationService = organizationService;
    }

    [HttpPost("AddOrganization")]
    public async Task<ApiResponse> AddOrganization(Add_Org_Dto addDTO, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        var serviceResult = await organizationService.AddAsync(addDTO, cancellationToken);
        return serviceResult;
    }

    [HttpPut("UpdateOrganization")]
    public async Task<ApiResponse> UpdateOrganization(Update_Org_Dto updateDTO, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        var serviceResult = await organizationService.UpdateAsync(updateDTO, cancellationToken);
        return serviceResult;
    }

    [HttpDelete("RemoveOrganization")]
    public async Task<ApiResponse> RemoveOrganization(Delete_Org_Dto dto, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        return await organizationService.DeleteAsync(dto, cancellationToken);
    }

    [HttpGet("GetAllOrganization")]
    public async Task<ApiResponse> GetAllOrganization(string? search = default, int? currentPage = default, int? pageSize = default, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());

        var getAllParams = new GetAllParams(search, currentPage, pageSize);
        return await organizationService.GetAllAsync(getAllParams, cancellationToken);
    }

    [HttpGet("GetOrganizationById")]
    public async Task<ApiResponse> GetOrganizationById(long Id, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        return await organizationService.GetAsync(Id, cancellationToken);
    }
}
