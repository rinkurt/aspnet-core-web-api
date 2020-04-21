using System;
using System.ComponentModel.DataAnnotations;

namespace Routine.Api.Validations
{
	public class ValidDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!(value is DateTime date)) 
			{
				return new ValidationResult(
					$"Invalid use of {nameof(ValidDateAttribute)}");
			}

			const int earliestYear = 1800;

			if (date.Year < earliestYear)
			{
				return new ValidationResult(
					$"Year of birth should be later than {earliestYear}.");
			}

			if (date > DateTime.Today)
			{
				return new ValidationResult(
					"Date of birth should be earlier than current date.");
			}

			return ValidationResult.Success;
		}
	}
}
