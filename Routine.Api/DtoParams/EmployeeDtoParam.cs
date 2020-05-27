using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.DtoParams
{
	public class EmployeeDtoParam
	{
		private const int MaxPageSize = 20;

		public string Gender { get; set; }
		public string Query { get; set; }
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 5;

		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = Math.Min(value, MaxPageSize);
		}

		public string OrderBy { get; set; } = "name";
	}
}
