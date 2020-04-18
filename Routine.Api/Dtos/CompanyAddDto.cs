using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Dtos
{
	public class CompanyAddDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(500)]
		public string Introduction { get; set; }

		public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();
	}
}
