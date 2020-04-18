using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.DtoParams;
using Routine.Api.Dtos;
using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
	[ApiController]
	[Route("/api/company")]
	public class CompanyController: ControllerBase
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly IMapper _mapper;

		public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
		{
			_companyRepository = companyRepository ?? 
			                     throw new ArgumentNullException(nameof(companyRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpGet]
		[HttpHead]
		public async Task<ActionResult<IEnumerable<CompanyDto>>>
			GetCompanies([FromQuery] CompanyDtoParam param)
		{
			var companies = await _companyRepository.GetCompaniesAsync(param);
			return Ok(_mapper.Map<IEnumerable<CompanyDto>>(companies));
		}

		[HttpGet("{companyId}", Name = nameof(GetCompany))]
		public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
		{
			var company = await _companyRepository.GetCompanyAsync(companyId);
			if (company == null)
			{
				return NotFound();
			}
			return Ok(_mapper.Map<CompanyDto>(company));
		}

		[HttpPost]
		public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyAddDto companyAddDto)
		{
			var company = _mapper.Map<Company>(companyAddDto);
			_companyRepository.AddCompany(company);
			await _companyRepository.SaveAsync();
			var companyDto = _mapper.Map<CompanyDto>(company);
			return CreatedAtRoute(nameof(GetCompany),
				new {companyId = companyDto.Id}, companyDto);
		}

		[HttpOptions]
		public IActionResult GetCompanyOptions()
		{
			Response.Headers.Add("Allow", "GET, HEAD, POST, OPTIONS");
			return Ok();
		}

		[HttpGet("/api/companies/({ids})", Name = nameof(GetCompaniesByIds))]
		public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompaniesByIds(
			[ModelBinder(BinderType = typeof(ArrayModelBinder))] [FromRoute]
			IEnumerable<Guid> ids)
		{
			if (ids == null)
			{
				return BadRequest();
			}

			var companyIds = ids as Guid[] ?? ids.ToArray();
			var companies = await _companyRepository.GetCompaniesAsync(companyIds);

			if (companyIds.Count() != companies.Count())
			{
				return NotFound();
			}

			var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

			return Ok(companyDtos);
		}

		[HttpPost("/api/companies")]
		public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanies(
			IEnumerable<CompanyAddDto> companyAddDtos)
		{
			var companies = _mapper.Map<IEnumerable<Company>>(companyAddDtos);

			foreach (var company in companies)
			{
				_companyRepository.AddCompany(company);
			}

			await _companyRepository.SaveAsync();

			var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);

			var idsStr = string.Join(",", companyDtos.Select(x => x.Id));

			return CreatedAtRoute(nameof(GetCompaniesByIds), 
				new { ids = idsStr }, 
				companyDtos);
		}
	}
}
