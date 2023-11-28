using AutoWrapper.Wrappers;
using Domain.ViewModels;

namespace Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<ApiResponse> AddAsync(AddEmployee addEmpDTO, CancellationToken cancellationToken = new());
}
