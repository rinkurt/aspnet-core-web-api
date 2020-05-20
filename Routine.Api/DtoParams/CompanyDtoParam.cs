using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Routine.Api.DtoParams
{
	public class CompanyDtoParam
	{
		private const int MaxPageSize = 20;

		public string Name { get; set; }
		public string Query { get; set; }

		public int PageNumber { get; set; } = 1;
		private int _pageSize = 5;

		public int PageSize {
			get => _pageSize;
			set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
		}

	}
}
