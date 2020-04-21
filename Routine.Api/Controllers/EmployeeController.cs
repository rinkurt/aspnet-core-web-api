using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> CreateOrUpdateEmployeeForCompany(
	        Guid companyId, Guid employeeId, EmployeeUpdateDto employeeUpdateDto)
        {
	        if (! await _companyRepository.CompanyExistsAsync(companyId))
	        {
		        return NotFound();
	        }

	        var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);

	        if (employee == null)
	        {
		        var employeeToAdd = _mapper.Map<Employee>(employeeUpdateDto);
		        employeeToAdd.Id = employeeId;

		        _companyRepository.AddEmployee(companyId, employeeToAdd);

		        await _companyRepository.SaveAsync();

		        var dtoToReturn = _mapper.Map<EmployeeDto>(employeeToAdd);

		        return CreatedAtRoute(nameof(GetEmployeeForCompany), new
		        {
			        companyId,
			        employeeId = dtoToReturn.Id
		        }, dtoToReturn);
	        }

	        _mapper.Map(employeeUpdateDto, employee);
	        _companyRepository.UpdateEmployee(employee);
	        await _companyRepository.SaveAsync();
	        return NoContent();
        }

        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
	        Guid companyId, Guid employeeId, JsonPatchDocument<EmployeeUpdateDto> jsonPatchDocument)
        {
	        if (! await _companyRepository.CompanyExistsAsync(companyId))
	        {
		        return NotFound();
	        }

	        var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);

	        if (employee == null)
	        {
		        var employeeUpdateDto = new EmployeeUpdateDto();
		        jsonPatchDocument.ApplyTo(employeeUpdateDto, ModelState);
		        if (!TryValidateModel(employeeUpdateDto))
		        {
			        return ValidationProblem(ModelState);
		        }

		        var employeeToAdd = _mapper.Map<Employee>(employeeUpdateDto);
		        employeeToAdd.Id = employeeId;
		        
		        _companyRepository.AddEmployee(companyId, employeeToAdd);
		        await _companyRepository.SaveAsync();

		        var dtoToReturn = _mapper.Map<EmployeeDto>(employeeToAdd);
		        return CreatedAtRoute(nameof(GetEmployeeForCompany), 
			        new {companyId, employeeId}, dtoToReturn);
	        }

	        var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employee);
	        
	        jsonPatchDocument.ApplyTo(dtoToPatch, ModelState);

	        if (!TryValidateModel(dtoToPatch))
	        {
		        return ValidationProblem(ModelState);
	        }

	        _mapper.Map(dtoToPatch, employee);
	        _companyRepository.UpdateEmployee(employee);
	        await _companyRepository.SaveAsync();

	        return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
	        if (! await _companyRepository.CompanyExistsAsync(companyId))
	        {
		        return NotFound();
	        }

	        var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);

	        if (employee == null)
	        {
		        return NotFound();
	        }
	        
	        _companyRepository.DeleteEmployee(employee);
	        await _companyRepository.SaveAsync();

	        return NoContent();
        }

        public override ActionResult ValidationProblem(ModelStateDictionary modelStateDictionary)
        {
	        var options = HttpContext.RequestServices
		        .GetRequiredService<IOptions<ApiBehaviorOptions>>();

	        return (ActionResult) options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}