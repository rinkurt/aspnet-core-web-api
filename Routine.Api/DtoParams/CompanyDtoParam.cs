using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Routine.Api.DtoParams
{
	public class CompanyDtoParam
	{
		public string Name { get; set; }

		public string Query { get; set; }
	}
}
