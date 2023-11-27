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

    [HttpPost]
    public async Task<ApiResponse> Add(Add_Org_Dto addDTO, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        var serviceResult = await organizationService.AddAsync(addDTO, cancellationToken);
        return serviceResult;
    }

    [HttpPut]
    public async Task<ApiResponse> Update(Update_Org_Dto updateDTO, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        var serviceResult = await organizationService.UpdateAsync(updateDTO, cancellationToken);
        return serviceResult;
    }

    [HttpDelete]
    public async Task<ApiResponse> Remove(Delete_Org_Dto dto, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        return await organizationService.DeleteAsync(dto, cancellationToken);
    }

    [HttpGet]
    public async Task<ApiResponse> GetAll(string? search = default, int? currentPage = default, int? pageSize = default, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());

        var getAllParams = new GetAllParams(search, currentPage, pageSize);
        return await organizationService.GetAllAsync(getAllParams, cancellationToken);
    }

    [HttpGet]
    [Route("{Id}")]
    public async Task<ApiResponse> Get(long Id, CancellationToken cancellationToken = new())
    {
        if (!ModelState.IsValid)
            throw new ApiException(ModelState.AllErrors());
        return await organizationService.GetAsync(Id, cancellationToken);
    }
}
