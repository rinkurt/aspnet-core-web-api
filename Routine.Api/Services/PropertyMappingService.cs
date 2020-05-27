using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Dtos;
using Routine.Api.Entities;

namespace Routine.Api.Services
{
	public class PropertyMappingService : IPropertyMappingService
	{
		private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping = 
			new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
			{
				{nameof(EmployeeDto.Id), new PropertyMappingValue(
					new List<string>{nameof(Employee.Id)})},
				{nameof(EmployeeDto.CompanyId), new PropertyMappingValue(
					new List<string>{nameof(Employee.CompanyId)})},
				{nameof(EmployeeDto.EmployeeNo), new PropertyMappingValue(
					new List<string>{nameof(Employee.EmployeeNo)})},
				{nameof(EmployeeDto.Name), new PropertyMappingValue(
					new List<string>{nameof(Employee.FirstName), nameof(Employee.LastName)})},
				{nameof(EmployeeDto.Gender), new PropertyMappingValue(
					new List<string>{nameof(Employee.Gender)})},
				{nameof(EmployeeDto.Age), new PropertyMappingValue(
					new List<string>{nameof(Employee.DateOfBirth)}, true)}
			};

		private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

		public PropertyMappingService()
		{
			_propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(_employeePropertyMapping));
		}

		public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSrc, TDest> ()
		{
			var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSrc, TDest>>();

			var propertyMappings = matchingMapping.ToList();

			if (propertyMappings.Count == 1)
			{
				return propertyMappings.First().MappingDictionary;
			}

			throw new Exception($"No Mapping Found: {typeof(TSrc)}, {typeof(TDest)}");
		}
	}
}
