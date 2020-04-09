using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
	[ApiController]
	[Route("/api/company")]
	public class CompanyController: ControllerBase
	{
		private readonly ICompanyRepository _companyRepository;

		public CompanyController(ICompanyRepository companyRepository)
		{
			_companyRepository = companyRepository ?? 
			                     throw new ArgumentNullException(nameof(companyRepository));
		}

		[HttpGet]
		public async Task<IActionResult> GetCompanies()
		{
			var companies = await _companyRepository.GetCompaniesAsync();
			return Ok(companies);
		}

		[HttpGet("{companyId}")]
		public async Task<IActionResult> GetCompany(Guid companyId)
		{
			var company = await _companyRepository.GetCompanyAsync(companyId);
			if (company == null)
			{
				return NotFound();
			}
			return Ok(company);
		}
	}
}
