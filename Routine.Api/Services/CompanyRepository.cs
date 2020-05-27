using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.DtoParams;
using Routine.Api.Dtos;
using Routine.Api.Entities;
using Routine.Api.Helpers;

namespace Routine.Api.Services
{
	public class CompanyRepository: ICompanyRepository
	{
		private readonly RoutineDbContext _context;
		private readonly IPropertyMappingService _propertyMappingService;

		public CompanyRepository(RoutineDbContext context, IPropertyMappingService propertyMappingService)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_propertyMappingService = propertyMappingService ??
				throw new ArgumentNullException(nameof(propertyMappingService));
		}

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParam param)
        {
	        if (param == null)
	        {
                throw new ArgumentNullException(nameof(CompanyDtoParam));
	        }

	        var items = _context.Companies.AsQueryable();

	        if (!string.IsNullOrWhiteSpace(param.Name))
	        {
		        items = items.Where(x => x.Name == param.Name.Trim());
	        }

	        if (!string.IsNullOrWhiteSpace(param.Query))
	        {
		        param.Query = param.Query.Trim();
		        items = items.Where(x => x.Name.Contains(param.Query) ||
		                                 x.Introduction.Contains(param.Query));
	        }

	        var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();

	        return await PagedList<Company>.CreateAsync(items, param.PageNumber, param.PageSize);
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Company>>
            GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await _context.Companies
                .Where(x => companyIds.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            company.Id = Guid.NewGuid();

            if (company.Employees != null)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = Guid.NewGuid();
                }
            }

            _context.Companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            // _context.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            _context.Companies.Remove(company);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(
	        Guid companyId, EmployeeDtoParam param)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            var items = _context.Employees.Where(x => x.CompanyId == companyId);
            
            if (param == null) return await items.OrderBy(x => x.EmployeeNo).ToListAsync();

            if (!string.IsNullOrWhiteSpace(param.Gender))
            {
	            var gender = Enum.Parse<Gender>(param.Gender.Trim());
	            items = items.Where(x => x.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace(param.Query))
            {
	            items = items.Where(x => x.EmployeeNo.Contains(param.Query) ||
	                                     x.FirstName.Contains(param.Query) || 
	                                     x.LastName.Contains(param.Query));
            }

            var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();
            items = items.ApplySort(param.OrderBy, mappingDictionary);
            
            return await items.ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await _context.Employees
                .Where(x => x.CompanyId == companyId && x.Id == employeeId)
                .FirstOrDefaultAsync();
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            // _context.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
