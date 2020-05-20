using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.DtoParams;
using Routine.Api.Entities;
using Routine.Api.Helpers;

namespace Routine.Api.Services
{
	public interface ICompanyRepository
	{
		Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParam param);
		Task<Company> GetCompanyAsync(Guid companyId);
		Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
		void AddCompany(Company company);
		void UpdateCompany(Company company);
		void DeleteCompany(Company company);
		Task<bool> CompanyExistsAsync(Guid companyId);

		Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParam param);
		Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
		void AddEmployee(Guid companyId, Employee employee);
		void UpdateEmployee(Employee employee);
		void DeleteEmployee(Employee employee);

		Task<bool> SaveAsync();
	}
}
