namespace Infrastructure.Services;
using Application.Common.Sender;
using Application.Interfaces.Services;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using SharedKernel.Helpers;

public record EmployeeService(IASender Sender) : IEmployeeService
{
    public async Task<ApiResponse> AddAsync(AddEmployee dto, CancellationToken cancellationToken = new())
    {
        var OrgIdResult = await Sender.Send(new CommandRequest<AddEmployee>(dto), cancellationToken);
        if (OrgIdResult.Status is Status.Exception)
            throw OrgIdResult.Exception!;

        return OrgIdResult.Result!;
    }
}
