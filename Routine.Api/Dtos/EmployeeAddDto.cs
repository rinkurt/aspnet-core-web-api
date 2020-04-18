using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Entities;

namespace Routine.Api.Dtos
{
	public class EmployeeAddDto: IValidatableObject
	{
		[Required]
		[MaxLength(10)]
		[Display(Name = "Employee No.")]
		public string EmployeeNo { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[MaxLength(50)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		public Gender Gender { get; set; }

		[Display(Name = "Date of Birth")]
		public DateTime DateOfBirth { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			const int maxLength = 80;
			if (FirstName.Length + LastName.Length > maxLength)
			{
				yield return new ValidationResult(
					$"Total length of name is larger than {maxLength}.",
					new []{nameof(FirstName), nameof(LastName)});
			}
		}
	}
}
