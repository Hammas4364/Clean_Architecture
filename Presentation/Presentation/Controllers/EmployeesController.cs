using Application.Interfaces.Services;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Helpers;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService empSerivce;

        public EmployeesController(IEmployeeService empSerivce)
        {
            this.empSerivce = empSerivce;
        }

        [HttpPost("AddEmployee")]
        public async Task<ApiResponse> AddEmployee(Add_Emp_Dto addDTO, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            var serviceResult = await empSerivce.AddAsync(addDTO, cancellationToken);
            return serviceResult;
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ApiResponse> UpdateEmployee(Update_Emp_Dto updateDTO, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            var serviceResult = await empSerivce.UpdateAsync(updateDTO, cancellationToken);
            return serviceResult;
        }

        [HttpPost("AddOrUpdateEmployee")]
        public async Task<ApiResponse> AddOrUpdateEmployee(Update_Emp_Dto updateDTO, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            var serviceResult = await empSerivce.AddOrUpdateAsync(updateDTO, cancellationToken);
            return serviceResult;
        }

        [HttpDelete("RemoveEmployee")]
        public async Task<ApiResponse> RemoveEmployee(Delete_Emp_Dto dto, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            return await empSerivce.DeleteAsync(dto, cancellationToken);
        }

        [HttpGet("GetAllEmployee")]
        public async Task<ApiResponse> GetAllEmployee(string? search = default, int? currentPage = default, int? pageSize = default, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());

            var getAllParams = new GetAllParams(search, currentPage, pageSize);
            return await empSerivce.GetAllAsync(getAllParams, cancellationToken);
        }

        [HttpGet("GetEmployeeByOrg")]
        public async Task<ApiResponse> GetEmployeeByOrg(string? search = default, int? currentPage = default, int? pageSize = default, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            var getAllParams = new GetAllParams(search, currentPage, pageSize);
            return await empSerivce.GetEmployeeByOrgAsync(getAllParams, cancellationToken);
        }

        [HttpGet("GetEmployeeById")]
        public async Task<ApiResponse> GetEmployeeById(long Id, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            return await empSerivce.GetAsync(Id, cancellationToken);
        }
    }
}
