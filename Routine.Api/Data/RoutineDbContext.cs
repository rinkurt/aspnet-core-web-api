using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;

namespace Routine.Api.Data
{
	public class RoutineDbContext : DbContext
	{
		public RoutineDbContext(DbContextOptions<RoutineDbContext> options) 
			: base(options)
		{
			
		}

		public DbSet<Company> Companies { get; set; }
		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>().Property(x => x.Name)
				.IsRequired().HasMaxLength(100);
			modelBuilder.Entity<Company>().Property(x => x.Introduction)
				.HasMaxLength(500);

			modelBuilder.Entity<Employee>().Property(x => x.EmployeeNo)
				.IsRequired().HasMaxLength(10);
			modelBuilder.Entity<Employee>().Property(x => x.FirstName)
				.IsRequired().HasMaxLength(50);
			modelBuilder.Entity<Employee>().Property(x => x.LastName)
				.IsRequired().HasMaxLength(50);

			modelBuilder.Entity<Employee>()
				.HasOne(x => x.Company)
				.WithMany(x => x.Employees)
				.HasForeignKey(x => x.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Company>().HasData(
				new Company
				{
					Id = Guid.Parse("0c131a59-dbec-4527-a92a-daeb910efecb"),
					Name = "Microsoft",
					Introduction = "Great Company"
				},
				new Company
				{
					Id = Guid.Parse("080d684e-21aa-46d8-8eb8-8c6709879f59"),
					Name = "Google",
					Introduction = "Don't be evil"
				},
				new Company
				{
					Id = Guid.Parse("d76af422-8f46-429c-b576-ad4d62216861"),
					Name = "Alibaba",
					Introduction = "Fubao Company"
				});
			
			modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    CompanyId = Guid.Parse("0c131a59-dbec-4527-a92a-daeb910efecb"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "MSFT231",
                    FirstName = "Nick",
                    LastName = "Carter",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                    CompanyId = Guid.Parse("0c131a59-dbec-4527-a92a-daeb910efecb"),
                    DateOfBirth = new DateTime(1981, 12, 5),
                    EmployeeNo = "MSFT245",
                    FirstName = "Vince",
                    LastName = "Carter",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    CompanyId = Guid.Parse("080d684e-21aa-46d8-8eb8-8c6709879f59"),
                    DateOfBirth = new DateTime(1986, 11, 4),
                    EmployeeNo = "G003",
                    FirstName = "Mary",
                    LastName = "King",
                    Gender = Gender.Female
                },
                new Employee
                {
                    Id = Guid.Parse("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"),
                    CompanyId = Guid.Parse("080d684e-21aa-46d8-8eb8-8c6709879f59"),
                    DateOfBirth = new DateTime(1977, 4, 6),
                    EmployeeNo = "G097",
                    FirstName = "Kevin",
                    LastName = "Richardson",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("679dfd33-32e4-4393-b061-f7abb8956f53"),
                    CompanyId = Guid.Parse("d76af422-8f46-429c-b576-ad4d62216861"),
                    DateOfBirth = new DateTime(1967, 1, 24),
                    EmployeeNo = "A009",
                    FirstName = "卡",
                    LastName = "里",
                    Gender = Gender.Female
                },
                new Employee
                {
                    Id = Guid.Parse("1861341e-b42b-410c-ae21-cf11f36fc574"),
                    CompanyId = Guid.Parse("d76af422-8f46-429c-b576-ad4d62216861"),
                    DateOfBirth = new DateTime(1957, 3, 8),
                    EmployeeNo = "A404",
                    FirstName = "Not",
                    LastName = "Man",
                    Gender = Gender.Male
                });
		}
	}
}
