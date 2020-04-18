using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.DtoParams;
using Routine.Api.Dtos;
using Routine.Api.Entities;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("/api/company/{companyId}/employee")]
    public class EmployeeController: ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeeController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? 
                                 throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> 
            GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeDtoParam param)
        {
	        if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepository.GetEmployeesAsync(companyId, param);
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("{employeeId}", Name = nameof(GetEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(
            Guid companyId, EmployeeAddDto employeeAddDto)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employee = _mapper.Map<Employee>(employeeAddDto);
            _companyRepository.AddEmployee(companyId, employee);
            await _companyRepository.SaveAsync();

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return CreatedAtRoute(nameof(GetEmployeeForCompany), new 
            {
	            companyId = companyId, employeeId = employee.Id
            }, employeeDto);
        }
    }
}