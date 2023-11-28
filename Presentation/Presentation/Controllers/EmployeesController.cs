using Application.Interfaces.Services;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService organizationService;

        public EmployeesController(IEmployeeService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpPost("AddEmp")]
        public async Task<ApiResponse> AddEmp(AddEmployee addDTO, CancellationToken cancellationToken = new())
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());
            var serviceResult = await organizationService.AddAsync(addDTO, cancellationToken);
            return serviceResult;
        }
    }
}
