using System;
using System.Collections.Generic;
using Routine.Api.Entities;

namespace Routine.Api.Dtos
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
    }
}